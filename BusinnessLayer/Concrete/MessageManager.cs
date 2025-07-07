using BusinnessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinnessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Message GetByID(int id)
        {
           return _messageDal.Get(m => m.MessageID == id);
        }


        public List<Message> GetListInbox(string mail)
        {
            return _messageDal.List(m => m.ReceiverMail == mail && m.MessageStatu == 2);
        }

        public List<Message> GetListInDraft(string mail)
        {
            return _messageDal.List(m => m.ReceiverMail == mail && m.MessageStatu == 1);
        }

        public List<Message> GetListSendbox(string mail)
        {
            return _messageDal.List(m => m.SenderMail == mail && m.MessageStatu == 2);
        }

        public List<Message> GetListInTrash(string mail)
        {
            return _messageDal.List(m => m.SenderMail == mail && m.MessageStatu == 0);
        }

        public void MessageAdd(Message message)
        {
            _messageDal.Insert(message);
        }

        public void MessageDelete(Message message)
        {
            _messageDal.Delete(message);
        }

        public void MessageUpdate(Message message)
        {
            _messageDal.Update(message);
        }
    }
}
