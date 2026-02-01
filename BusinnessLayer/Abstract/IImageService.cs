using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinnessLayer.Abstract
{
    public interface IImageService
    {
        List<ImageFile> GetList();
    }
}
