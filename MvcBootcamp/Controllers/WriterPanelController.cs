using BusinnessLayer.Concrete;
using BusinnessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MvcBootcamp.Controllers
{
    public class WriterPanelController : Controller
    {
        // GET: WriterPanel
        // Initialize business layer managers with Entity Framework data access
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        Context c = new Context(); // Database context for direct operations

        [HttpGet]
        public ActionResult WriterProfile()
        {
            // Get writer's email from session and fetch profile data
            string mail = (string)Session["WriterMail"];
            var writer = wm.GetWriterByMail(mail);
            return View(writer);
        }

        [HttpPost]
        public async Task<ActionResult> WriterProfile(Writer writer)
        {
            // Retrieve existing writer to preserve image if not changed
            var existingWriter = wm.GetByID(writer.WriterID);
            writer.WriterImage = existingWriter.WriterImage;

            // Handle image upload if new image is provided
            if (writer.Image != null && writer.Image.ContentLength > 0)
            {
                var extension = Path.GetExtension(writer.Image.FileName);
                var newImageName = Guid.NewGuid() + extension; // Generate unique filename

                var uploadPath = Server.MapPath("~/userimage/");

                // Create directory if it doesn't exist
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var savePath = Path.Combine(uploadPath, newImageName);

                // Save uploaded file to server
                writer.Image.SaveAs(savePath);

                // Update image URL in the writer object
                existingWriter.ImageUrl = "/userimage/" + newImageName;
            }
            else
            {
                // Keep existing image if no new image uploaded
                existingWriter.ImageUrl = existingWriter.ImageUrl;
            }

            // Preserve password if not changed in form
            if (string.IsNullOrEmpty(writer.WriterPassword))
            {
                string writerPassword = Session["WriterPassword"] as string;
                writer.WriterPassword = writerPassword;
            }

            // Validate writer data using FluentValidation
            WriterValidator writerValidator = new WriterValidator();
            ValidationResult results = writerValidator.Validate(writer);
            if (results.IsValid)
            {
                wm.UpdateWriterFields(writer);
                return RedirectToAction("WriterProfile", "WriterPanel");
            }
            else
            {
                // Add validation errors to ModelState for display
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(writer);
        }

        // Display headings created by current writer with pagination
        public ActionResult MyHeading(string p, int page = 1)
        {
            var writer = wm.GetWriterByMail((string)Session["WriterMail"]);
            var writerIdInfo = writer.WriterID;
            var contentValues = hm.GetListByWriter(writerIdInfo).ToPagedList(page, 10);
            return View(contentValues);
        }

        // Helper method to create dropdown list of categories
        List<SelectListItem> categoryList()
        {
            List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            return categoryValues;
        }

        [HttpGet]
        public ActionResult NewHeading()
        {
            // Get category list for dropdown in view
            List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();

            ViewBag.categoryValues1 = categoryValues;
            return View();
        }

        [HttpPost]
        public ActionResult NewHeading(Heading heading)
        {
            // Get current writer's ID from session
            var writer = wm.GetWriterByMail((string)Session["WriterMail"]);
            var writerIdInfo = writer.WriterID;

            // Set heading properties before saving
            heading.HeadingDate = DateTime.Now;
            heading.WriterID = writerIdInfo;
            heading.HeadingStatus = true; // Active status
            hm.HeadingAdd(heading);
            return RedirectToAction("MyHeading");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            var HeadingValue = hm.GetByID(id);
            var categoryList1 = categoryList();

            // Preselect current category in dropdown
            foreach (var category in categoryList1)
            {
                if (category.Value == HeadingValue.CategoryId.ToString())
                {
                    category.Selected = true;
                }
            }

            ViewBag.categoryValues = categoryList1;
            return View(HeadingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {
            // Update heading with current timestamp and default values
            heading.HeadingDate = DateTime.Now;
            heading.WriterID = 1; // Hardcoded writer ID - might need improvement
            heading.HeadingStatus = true;
            hm.HeadingUpdate(heading);
            return RedirectToAction("MyHeading");
        }

        // Soft delete heading by setting status to false
        public ActionResult HeadingDelete(int id)
        {
            var headingValue = hm.GetByID(id);
            headingValue.HeadingStatus = false; // Soft delete (archive instead of permanent delete)
            hm.HeadingDelete(headingValue);
            return RedirectToAction("MyHeading");
        }

        // View all headings with pagination
        public ActionResult AllHeading(int page = 1)
        {
            var headings = hm.GetList().ToPagedList(page, 10);
            return View(headings);
        }
    }
}