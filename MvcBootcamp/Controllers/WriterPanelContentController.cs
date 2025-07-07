using BusinnessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBootcamp.Controllers
{
    public class WriterPanelContentController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        // GET: WriterPanelContent

        Context cxt = new Context();

        public string AdminUserName()
        {
            string userName = Session["WriterMail"] as string;
            return userName;
        }

        public ActionResult MyContent(string p)
        {
            Context c = new Context();
            p = (string)Session["WriterMail"];
            var writerIdInfo = c.Writers.Where(x=>x.WriterMail == p)
                .Select(y=>y.WriterID).FirstOrDefault();
            var contentValues = cm.GetListByWriter(writerIdInfo);

            return View(contentValues);
        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.d=id;
            return View();
        }

        [HttpPost]
        public ActionResult AddContent(Content c)
        {
            c.ContentStatus = true;
            c.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            var writerMail = (string)Session["WriterMail"];
            var writerIdInfo = cxt.Writers.Where(w => w.WriterMail == writerMail)
                .Select(w => w.WriterID).FirstOrDefault();
            c.WriterID = writerIdInfo;
            cm.ContentAdd(c);
            return RedirectToAction("MyContent");
        }
        public ActionResult ToDoList()
        {
            return View();
        }
    }
}