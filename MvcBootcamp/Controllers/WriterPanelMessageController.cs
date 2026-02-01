using BusinnessLayer.Concrete;
using BusinnessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
using System;
using System.Web.Mvc;

namespace MvcBootcamp.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        // Dependency injection for message operations
        MessageManager mm = new MessageManager(new EfMessageDal());

        // Validator for message entity
        MessageValidator messageValidator = new MessageValidator();

        // Helper method to get current writer's username from session
        public string WriterUserName()
        {
            string userName = Session["WriterMail"] as string;
            return userName;
        }

        // GET: WriterPanelMessage
        public ActionResult Inbox(int page = 1)
        {
            string user = WriterUserName();
            // Get paginated inbox messages
            var messageList = mm.GetListInbox(user).ToPagedList(page, 10);
            return View(messageList);
        }

        public PartialViewResult MessageListMenu()
        {
            string userName = WriterUserName();

            // Calculate message counts for different categories
            var messageList = mm.GetListInbox(userName);
            var inComingMessageCount = messageList.Count;

            var messageList1 = mm.GetListSendbox(userName);
            var outComingMessageCount = messageList1.Count;

            var messageList2 = mm.GetListInDraft(userName);
            var draftMessageCount = messageList2.Count;

            var messageList3 = mm.GetListInTrash(userName);
            var trashMessageCount = messageList3.Count;

            // Store counts in session for persistence
            Session["inComingMessageCount"] = inComingMessageCount;
            Session["outComingMessageCount"] = outComingMessageCount;
            Session["draftMessageCount"] = draftMessageCount;
            Session["trashMessageCount"] = trashMessageCount;

            // Pass counts to view via ViewBag
            ViewBag.inComingMessageCount = Session["inComingMessageCount"];
            ViewBag.outComingMessageCount = Session["outComingMessageCount"];
            ViewBag.draftMessageCount = Session["draftMessageCount"];
            ViewBag.trashMessageCount = Session["trashMessageCount"];

            return PartialView();
        }

        public ActionResult Sendbox(int page = 1)
        {
            string user = WriterUserName();
            // Get paginated sent messages
            var messageList = mm.GetListSendbox(user).ToPagedList(page, 10);
            return View(messageList);
        }

        // Get detailed view of a specific message
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
            message.MessageStatu = 2; // Status 2 likely means "sent"

            // Validate message using FluentValidation
            ValidationResult results = messageValidator.Validate(message);
            if (results.IsValid)
            {
                // Set current date without time component
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

        public ActionResult Draft(int page = 1)
        {
            var mail = WriterUserName();

            // Get paginated draft messages
            var messageList = mm.GetListInDraft(mail).ToPagedList(page, 10);
            return View(messageList);
        }

        // Load draft message for editing
        public ActionResult DraftMessage(int id)
        {
            var newMessage = mm.GetByID(id);

            return View("NewMessage", newMessage);
        }

        public ActionResult SaveInDraft(Message message)
        {
            message.MessageStatu = 1; // Status 1 likely means "draft"
            ValidationResult results = messageValidator.Validate(message);
            if (results.IsValid)
            {
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(message);
                return RedirectToAction("Inbox");
            }
            else
            {
                // Handle validation errors
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return RedirectToAction("Draft", "Message");
        }

        public ActionResult Trash(int page = 1)
        {
            var userName = WriterUserName();
            // Get paginated trashed messages
            var messageList = mm.GetListInTrash(userName).ToPagedList(page, 10);
            return View(messageList);
        }

        // Permanently delete all messages in trash
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