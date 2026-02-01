using BusinnessLayer.Concrete;
using BusinnessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using FluentValidation.Results;
using PagedList;
using System;
using System.Web.Mvc;
using Message = EntityLayer.Concrete.Message;

namespace MvcBootcamp.Controllers
{
    public class MessageController : Controller
    {
        // Message manager for handling message operations
        MessageManager mm = new MessageManager(new EfMessageDal());

        // Validator for message entity using FluentValidation
        MessageValidator messageValidator = new MessageValidator();

        // Helper method to get admin username from session
        public string AdminUserName()
        {
            string userName = Session["AdminUserName"] as string;
            return userName;
        }

        // GET: Message
        // Display inbox messages with pagination
        public ActionResult Inbox(int page = 1)
        {
            var mail = AdminUserName();
            var messageList = mm.GetListInbox(mail).ToPagedList(page, 10);
            return View(messageList);
        }

        // Display sent messages with pagination
        public ActionResult Sendbox(int page = 1)
        {
            var mail = AdminUserName();
            var messageList = mm.GetListSendbox(mail).ToPagedList(page, 10);
            return View(messageList);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)] // Allows HTML input (for rich text editors)
        public ActionResult NewMessage(Message message)
        {
            var mail = AdminUserName();
            message.MessageStatu = 2; // Status 2 = Sent message
            message.SenderMail = mail;

            // Validate message using FluentValidation
            ValidationResult results = messageValidator.Validate(message);
            if (results.IsValid)
            {
                // Set message date to current date (without time)
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(message);
                return RedirectToAction("Inbox");
            }
            else
            {
                // Add validation errors to ModelState
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        // Display draft messages with pagination
        public ActionResult Draft(int page = 1)
        {
            var mail = AdminUserName();
            var messageList = mm.GetListInDraft(mail).ToPagedList(page, 10);
            return View(messageList);
        }

        // View details of a specific message
        public ActionResult GetMessageDetails(int id)
        {
            var messageDetails = mm.GetByID(id);
            return View(messageDetails);
        }

        // Load draft message for editing
        public ActionResult DraftMessage(int id)
        {
            var newMessage = mm.GetByID(id);
            return View("NewMessage", newMessage); // Reuse NewMessage view for editing
        }

        // Save message as draft
        public ActionResult SaveInDraft(Message message)
        {
            message.MessageStatu = 1; // Status 1 = Draft message
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

        // Display trash/deleted messages with pagination
        public ActionResult Trash(int page = 1)
        {
            var userName = AdminUserName();
            var messageList = mm.GetListInTrash(userName).ToPagedList(page, 10);
            return View(messageList);
        }

        // Permanently delete all messages in trash
        public ActionResult EmptyTrash()
        {
            var messages = mm.GetListInTrash(AdminUserName());
            foreach (var message in messages)
            {
                mm.MessageDelete(message);
            }

            return RedirectToAction("Inbox");
        }

    }
}