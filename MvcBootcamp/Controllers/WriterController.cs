using BusinnessLayer.Concrete;
using BusinnessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System.Linq;
using System.Web.Mvc;

namespace MvcBootcamp.Controllers
{
    public class WriterController : Controller
    {
        // Initialize WriterManager with Entity Framework data access layer
        WriterManager wm = new WriterManager(new EfWriterDal());

        public ActionResult Index()
        {
            // Get all writers from database
            var WriterValues = wm.GetList();
            return View(WriterValues);
        }

        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWriter(Writer writer)
        {
            // Validate writer using FluentValidation rules
            WriterValidator writerValidator = new WriterValidator();
            ValidationResult results = writerValidator.Validate(writer);

            if (results.IsValid)
            {
                wm.WriterAdd(writer);
                return RedirectToAction("Index");
            }
            else
            {
                // Add validation errors to ModelState for display in view
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            // Get writer by ID for editing
            var writer = wm.GetByID(id);
            return View(writer);
        }

        [HttpPost]
        public ActionResult EditWriter(Writer writer)
        {
            WriterValidator writerValidator = new WriterValidator();
            ValidationResult results = writerValidator.Validate(writer);

            // Preserve existing password if field is empty (not changed)
            if (string.IsNullOrEmpty(writer.WriterPassword))
            {
                var password = wm.GetList().FirstOrDefault(w => w.WriterID == writer.WriterID);
                writer.WriterPassword = password.WriterPassword;
            }

            if (results.IsValid)
            {
                // Update writer fields in repository
                wm.UpdateWriterFields(writer);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }

    }
}