using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinnessLayer.Abstract
{
    public interface IMessageService
    {
        List<Message> GetListInbox(string mail);
        List<Message> GetListInDraft(string mail);
        List<Message> GetListSendbox(string mail);
        List<Message> GetListInTrash(string mail);
        void MessageAdd(Message message);
        Message GetByID(int id);
        void MessageDelete(Message message);
        void MessageUpdate(Message message);

    }
}
