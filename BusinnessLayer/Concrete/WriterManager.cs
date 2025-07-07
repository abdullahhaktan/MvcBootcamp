using BusinnessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinnessLayer.Concrete
{
    public class WriterManager : IWriterService
    {

        IWriterDal _writerDal;

        public WriterManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public Writer GetByID(int id)
        {
            return _writerDal.Get(x=>x.WriterID == id);
        }

        public List<Writer> GetList()
        {
           return _writerDal.List();
        }

        public void WriterAdd(Writer writer)
        {
           _writerDal.Insert(writer);
        }

        public void WriterDelete(Writer writer)
        {
            _writerDal.Delete(writer);
        }

        public void WriterUpdate(Writer writer)
        {
            _writerDal.Update(writer);
        }

        public void UpdateWriterFields(Writer writer)
        {
            var existingWriter = _writerDal.Get(x => x.WriterID == writer.WriterID);
            if (existingWriter != null)
            {
                existingWriter.WriterName = writer.WriterName;
                existingWriter.WriterSurname = writer.WriterSurname;
                existingWriter.WriterTitle = writer.WriterTitle;
                existingWriter.WriterAbout = writer.WriterAbout;
                existingWriter.WriterMail = writer.WriterMail;
                existingWriter.WriterPassword = writer.WriterPassword;
                existingWriter.WriterImage = writer.WriterImage;
                _writerDal.Update(existingWriter);
            }
        }

        public Writer GetWriterByMail(string mail)
        {
            var writer = _writerDal.List().Where(w=>w.WriterMail == mail).FirstOrDefault();
            return writer;
        }

    }
}
