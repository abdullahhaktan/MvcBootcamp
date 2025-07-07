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
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MvcBootcamp.Controllers
{
    public class WriterPanelController : Controller
    {
        // GET: WriterPanel
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        Context c = new Context();

        [HttpGet]
        public ActionResult WriterProfile()
        {
            string mail = (string)Session["WriterMail"];

            var writer = wm.GetWriterByMail(mail);

            return View(writer);
        }

        [HttpPost]
        public async Task<ActionResult> WriterProfile(Writer writer)
        {
            var existingWriter = wm.GetByID(writer.WriterID);

            writer.WriterImage = existingWriter.WriterImage;

            if (writer.Image != null && writer.Image.ContentLength > 0) 
            {
                var extension = Path.GetExtension(writer.Image.FileName);
                var newImageName = Guid.NewGuid() + extension;


                var uploadPath = Server.MapPath("~/userimage/");

                // Klasör yoksa oluştur
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var savePath = Path.Combine(uploadPath, newImageName);

                writer.Image.SaveAs(savePath); 

                existingWriter.ImageUrl = "/userimage/" + newImageName;
            }
            else
            {
                existingWriter.ImageUrl = existingWriter.ImageUrl;
            }


            if (string.IsNullOrEmpty(writer.WriterPassword))
            {
                string writerPassword = Session["WriterPassword"] as string;
                writer.WriterPassword = writerPassword;
            }

            WriterValidator writerValidator = new WriterValidator();
            ValidationResult results = writerValidator.Validate(writer);
            if (results.IsValid)
            {
                wm.UpdateWriterFields(writer);
                return RedirectToAction("WriterProfile", "WriterPanel");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(writer);
        }


        public ActionResult MyHeading(string p , int page=1)
        {
            var writer = wm.GetWriterByMail((string)Session["WriterMail"]);

            var writerIdInfo = writer.WriterID;

            var contentValues = hm.GetListByWriter(writerIdInfo).ToPagedList(page,10);
            return View(contentValues);
        }

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
            var writer = wm.GetWriterByMail((string)Session["WriterMail"]);

            var writerIdInfo = writer.WriterID;

            heading.HeadingDate = DateTime.Now;
            heading.WriterID = writerIdInfo;
            heading.HeadingStatus = true;
            hm.HeadingAdd(heading);
            return RedirectToAction("MyHeading");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            var HeadingValue = hm.GetByID(id);

            var categoryList1 = categoryList();

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
            heading.HeadingDate = DateTime.Now;
            heading.WriterID = 1;
            heading.HeadingStatus = true;

            hm.HeadingUpdate(heading);
            return RedirectToAction("MyHeading");
        }

        public ActionResult HeadingDelete(int id)
        {
            var headingValue = hm.GetByID(id);
            headingValue.HeadingStatus = false; // Soft delete
            hm.HeadingDelete(headingValue);
            return RedirectToAction("MyHeading");
        }
        public ActionResult AllHeading(int page = 1)
        {
            var headings = hm.GetList().ToPagedList(page,10);
            return View(headings);
        }

    }
}