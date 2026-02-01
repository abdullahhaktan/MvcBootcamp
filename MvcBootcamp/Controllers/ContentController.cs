using BusinnessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace MvcBootcamp.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content

        ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult Index()
        {
            return View();
        }

        Context c = new Context();
        public ActionResult GetAllContent()
        {
            var values = c.Contents.ToList();
            return View(values);
        }

        public ActionResult ContentByHeading(int id)
        {
            var contentValues = cm.GetListByHeadingId(id);
            var contentHeading = c.Headings.Where(x => x.HeadingID == id).Select(y => y.HeadingName).FirstOrDefault();
            ViewBag.heading = contentHeading;
            return View(contentValues);
        }

    }
}