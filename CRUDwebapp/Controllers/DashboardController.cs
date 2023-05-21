using Microsoft.AspNetCore.Mvc;

namespace CRUDwebapp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            return View();
        }
    }
}
