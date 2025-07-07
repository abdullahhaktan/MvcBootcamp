using BusinnessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBootcamp.Controllers
{
    public class GaleryController : Controller
    {
        // GET: Galery
        ImageFileManager ifm = new ImageFileManager(new EfImageFileDal());
        public ActionResult Index()
        {
            var imageFiles = ifm.GetList();
            return View(imageFiles);
        }
    }
}