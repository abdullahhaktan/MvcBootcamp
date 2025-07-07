using BusinnessLayer.Concrete;
using BusinnessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using EntityLayer.Concrete;
using Message = EntityLayer.Concrete.Message;
using PagedList;

namespace MvcBootcamp.Controllers
{
    public class MessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());

        MessageValidator messageValidator = new MessageValidator();

        public string AdminUserName()
        {
            string userName = Session["AdminUserName"] as string;
            return userName;
        }

        // GET: Message
        public ActionResult Inbox(int page = 1)
        {
            var mail = AdminUserName();
            var messageList = mm.GetListInbox(mail).ToPagedList(page,10);
            return View(messageList);
        }

        public ActionResult Sendbox(int page = 1)
        { 
            var mail = AdminUserName();
            var messageList = mm.GetListSendbox(mail).ToPagedList(page,10);
            return View(messageList);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewMessage(Message message)
        {
            var mail = AdminUserName();
            message.MessageStatu = 2;
            message.SenderMail = mail;
            ValidationResult results = messageValidator.Validate(message);
            if(results.IsValid)
            {
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(message);
                return RedirectToAction("Inbox");
            }
            else
            {
                foreach(var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult Draft(int page = 1)
        {
            var mail = AdminUserName();

            var messageList = mm.GetListInDraft(mail).ToPagedList(page,10);
            return View(messageList);
        }

        public ActionResult GetMessageDetails(int id)
        {
            var messageDetails = mm.GetByID(id);
            return View(messageDetails);
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

            return RedirectToAction("Draft","Message");
        }

        public ActionResult Trash(int page=1)
        {
            var userName = AdminUserName();
            var messageList = mm.GetListInTrash(userName).ToPagedList(page,10);
            return View(messageList);
        }

        public ActionResult EmptyTrash()
        {
            var messages = mm.GetListInTrash(AdminUserName());
            foreach(var message in messages)
            {
                mm.MessageDelete(message);
            }

            return RedirectToAction("Inbox");
        }

    }
}