using BusinnessLayer.Concrete;
using BusinnessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBootcamp.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        ContactManager cm = new ContactManager(new EfContactDal());

        MessageManager mm = new MessageManager(new EfMessageDal());

        ContactValidator cv = new ContactValidator();

        public string AdminUserName()
        {
            string userName = Session["AdminUserName"] as string;
            return userName;
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactValues = cm.GetByID(id);
            return View(contactValues);
        }

        public PartialViewResult MessageListMenu()
        {
            string userName = AdminUserName();
            var messageList = mm.GetListInbox(userName);
            var inComingMessageCount = messageList.Count;

            var messageList1 = mm.GetListSendbox(userName);
            var outComingMessageCount = messageList1.Count;

            var messageList2 = mm.GetListInDraft(userName);
            var draftMessageCount = messageList2.Count;

            var messageList3 = mm.GetListInTrash(userName);
            var trashMessageCount = messageList3.Count;

            Session["inComingMessageCount"] = inComingMessageCount;
            Session["outComingMessageCount"] = outComingMessageCount;
            Session["draftMessageCount"] = draftMessageCount;
            Session["trashMessageCount"] = trashMessageCount;

            ViewBag.inComingMessageCount = Session["inComingMessageCount"];
            ViewBag.outComingMessageCount = Session["outComingMessageCount"];
            ViewBag.draftMessageCount = Session["draftMessageCount"];
            ViewBag.trashMessageCount = Session["trashMessageCount"];
            return PartialView();
        }

    }
}