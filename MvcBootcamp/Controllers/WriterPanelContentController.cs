using BusinnessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MvcBootcamp.Controllers
{
    public class WriterPanelContentController : Controller
    {
        // Content manager for business logic operations
        ContentManager cm = new ContentManager(new EfContentDal());
        // GET: WriterPanelContent

        // Database context for direct database operations
        Context cxt = new Context();

        // Helper method to get admin/writer username from session
        public string AdminUserName()
        {
            string userName = Session["WriterMail"] as string;
            return userName;
        }

        // Display content created by current writer
        public ActionResult MyContent(string p)
        {
            Context c = new Context();
            // Get writer email from session
            p = (string)Session["WriterMail"];

            // Query database to get writer ID based on email
            var writerIdInfo = c.Writers.Where(x => x.WriterMail == p)
                .Select(y => y.WriterID).FirstOrDefault();

            // Get content list filtered by writer ID
            var contentValues = cm.GetListByWriter(writerIdInfo);

            return View(contentValues);
        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            // Pass heading ID to view for content association
            ViewBag.d = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddContent(Content c)
        {
            // Set content properties before saving
            c.ContentStatus = true; // Active content
            c.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString()); // Current date only (no time)

            // Get writer ID from session email
            var writerMail = (string)Session["WriterMail"];
            var writerIdInfo = cxt.Writers.Where(w => w.WriterMail == writerMail)
                .Select(w => w.WriterID).FirstOrDefault();

            c.WriterID = writerIdInfo; // Associate content with writer

            cm.ContentAdd(c); // Save to database

            return RedirectToAction("MyContent");
        }

        // Placeholder for ToDoList functionality
        public ActionResult ToDoList()
        {
            return View();
        }
    }
}