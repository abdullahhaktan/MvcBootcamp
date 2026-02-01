using BusinnessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinnessLayer.Concrete
{
    public class WritrLoginManager : IWriterLoginService
    {
        IWriterDal _writerDal;

        public WritrLoginManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public Writer GetWriter(string username, string password)
        {
            return _writerDal.Get(x => x.WriterMail == username && x.WriterPassword == password);
        }
    }
}
