using BusinnessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBootcamp.Controllers
{
    public class IstatistikController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryDal());

        HeadingManager hm = new HeadingManager(new EfHeadingDal());

        // GET: Istatistik
        public ActionResult Index()
        {
            var totalCategoryCount = cm.GetList().Count;

            ViewBag.totalCategoryCount = totalCategoryCount;

            var SoftwareHeadingCount = cm.GetList().Where(c => c.CategoryName == "Yazılım").Count();
            
            ViewBag.softwareCount = SoftwareHeadingCount;

            var containsLetterA = cm.GetList().Where(c => c.CategoryName.ToLower().Contains("a")).Count();

            ViewBag.containsLetterA = containsLetterA;

            var categoryNameThatHasMostTitle = hm.GetList()
                .GroupBy(h => h.Category.CategoryName)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            ViewBag.categoryNameThatHasMostTitle = categoryNameThatHasMostTitle;

            var activeCategoryCount = cm.GetList().Where(c => c.CategoryStatus == true).Count();

            var pasiveCategoryCount = cm.GetList().Where(c => c.CategoryStatus == false).Count();

            var difference = activeCategoryCount - pasiveCategoryCount;

            ViewBag.difference = difference;

            return View();
        }
    }
}