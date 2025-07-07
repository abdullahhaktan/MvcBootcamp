using BusinnessLayer.Concrete;
using BusinnessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBootcamp.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());

        MessageValidator messageValidator = new MessageValidator();

        public string WriterUserName()
        {
            string userName = Session["WriterMail"] as string;
            return userName;
        }

        // GET: WriterPanelMessage
        public ActionResult Inbox(int page=1)
        {
            string user = WriterUserName();
            var messageList = mm.GetListInbox(user).ToPagedList(page,10);
            return View(messageList);
        }
        public PartialViewResult MessageListMenu()
        {
            string userName = WriterUserName();

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

        public ActionResult Sendbox(int page=1)
        {
            string user = WriterUserName();
            var messageList = mm.GetListSendbox(user).ToPagedList(page,10);
            return View(messageList);
        }

        public ActionResult GetMessageDetails(int id)
        {
            var messageDetails = mm.GetByID(id);
            return View(messageDetails);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            message.SenderMail = WriterUserName();
            message.MessageStatu = 2;
            ValidationResult results = messageValidator.Validate(message);
            if (results.IsValid)
            {
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(message);
                return RedirectToAction("Inbox");
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

        public ActionResult Draft(int page=1)
        {
            var mail = WriterUserName();

            var messageList = mm.GetListInDraft(mail).ToPagedList(page,10);
            return View(messageList);
        }

        public ActionResult DraftMessage(int id)
        {
            var newMessage = mm.GetByID(id);

            return View("NewMessage", newMessage);
        }

        public ActionResult SaveInDraft(Message message)
        {
            message.MessageStatu = 1;
            ValidationResult results = messageValidator.Validate(message);
            if (results.IsValid)
            {
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(message);
                return RedirectToAction("Inbox");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return RedirectToAction("Draft", "Message");
        }

        public ActionResult Trash(int page=1)
        {
            var userName = WriterUserName();
            var messageList = mm.GetListInTrash(userName).ToPagedList(page,10);
            return View(messageList);
        }

        public ActionResult EmptyTrash()
        {
            var messages = mm.GetListInTrash(WriterUserName());
            foreach (var message in messages)
            {
                mm.MessageDelete(message);
            }

            return RedirectToAction("Inbox");
        }

    }
}