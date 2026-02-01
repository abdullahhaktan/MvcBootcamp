using System.Web.Mvc;

namespace MvcBootcamp.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: ErrorPage
        public ActionResult Page403()
        {
            Response.StatusCode = 403; // Forbidden
            Response.TrySkipIisCustomErrors = true; // Skip IIS custom errors
            return View();
        }

        public ActionResult Page404()
        {
            Response.StatusCode = 404; // Not Found
            Response.TrySkipIisCustomErrors = true; // Skip IIS custom errors
            return View();
        }

    }
}