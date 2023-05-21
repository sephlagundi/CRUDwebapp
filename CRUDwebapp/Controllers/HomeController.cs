using CRUDwebapp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace CRUDwebapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoginUser(UserInfo user)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://localhost:5272/api/Token", content))
                {
                    /*HttpContext.Session.SetString("JWToken", token);*/
                    string token = await response.Content.ReadAsStringAsync();
                    if (token == "Invalid Credentials")
                    {
                        ViewBag.Message = "Incorrect UsedId or Password";
                        return Redirect("~/Home/Index");
                    }

                    HttpContext.Session.SetString("JWToken", token);


                }

                return Redirect("~/Dashboard/Index");
            }
        }


        public IActionResult LogOff()
        {
            HttpContext.Session.Clear(); //clear token
            return Redirect("~/Dashboard/Index");
        }










        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}