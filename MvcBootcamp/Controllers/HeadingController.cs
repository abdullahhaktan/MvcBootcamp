using BusinnessLayer.Concrete;
using BusinnessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBootcamp.Controllers
{
    public class HeadingController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        HeadingValidator hv = new HeadingValidator();

        public ActionResult Index(int page = 1)
        {
            var headingValues = hm.GetList().ToPagedList(page,10);
            return View(headingValues);
        }

        List<SelectListItem> categoryList()
        {
            List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            return categoryValues;
        }

        List<SelectListItem> writerList()
        {
            List<SelectListItem> writerValues = (from x in wm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.WriterName + " " +x.WriterSurname,
                                                       Value = x.WriterID.ToString()
                                                   }).ToList();
            return writerValues;
        }

        [HttpGet]
        public ActionResult AddHeading()
        {
            ViewBag.categoryValues = categoryList();

            ViewBag.writerValues = writerList();

            return View();
        }

        [HttpPost]
        public ActionResult AddHeading(Heading heading)
        {
            hm.HeadingAdd(heading);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            var HeadingValue = hm.GetByID(id);

            var categoryList1 = categoryList();

            foreach(var category in categoryList1)
            {
                if(category.Value == HeadingValue.CategoryId.ToString())
                {
                    category.Selected = true;
                }
            }

            ViewBag.categoryValues = categoryList1;
            
            return View(HeadingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {
            hm.HeadingUpdate(heading);
            return RedirectToAction("Index");

        }

        public ActionResult HeadingDelete(int id)
        {
            var headingValue = hm.GetByID(id);
            if(headingValue.HeadingStatus == true)
            {
                headingValue.HeadingStatus = false;
            }
            else
            {
                headingValue.HeadingStatus = true;
            }
            hm.HeadingDelete(headingValue);
            return RedirectToAction("Index");
        }
    }
}