using BusinnessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinnessLayer.Concrete
{
    public class ImageFileManager : IImageService
    {
        IImageFileDal _imageFileDal;
        public ImageFileManager(EfImageFileDal imageFileDal)
        {
            _imageFileDal = imageFileDal;
        }

        public List<ImageFile> GetList()
        {
            return _imageFileDal.List();
        }
    }
}
