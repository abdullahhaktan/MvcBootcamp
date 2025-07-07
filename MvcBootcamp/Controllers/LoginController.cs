using BusinnessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcBootcamp.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        WritrLoginManager wlm = new WritrLoginManager(new EfWriterDal());
        AdminLoginManager alm = new AdminLoginManager(new EfAdminDal());
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            var adminuserinfo = alm.GetAdmin(admin.AdminUserName, admin.AdminPassword);
            if (adminuserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUserName, false);
                Session["AdminUserName"] = adminuserinfo.AdminUserName;
                Session["AdminId"] = adminuserinfo.AdminID;

                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriterLogin(Writer w)
        {
            var writerUserInfo = wlm.GetWriter(w.WriterMail, w.WriterPassword);
            if (writerUserInfo != null)
            {
                FormsAuthentication.SetAuthCookie(w.WriterMail, false); // we ensure that the user informations will be gone if browser is closed
                Session["WriterMail"] = writerUserInfo.WriterMail; // we can use this session in other controllers
                Session["WriterPassword"] = writerUserInfo.WriterPassword;
                return RedirectToAction("MyHeading", "WriterPanel");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut(); // this will clear the authentication cookie
            Session.Abandon(); // this will clear the session
            return RedirectToAction("Headings", "Default");
        }
    }
}