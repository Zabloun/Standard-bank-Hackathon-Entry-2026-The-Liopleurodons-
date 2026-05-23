using Microsoft.AspNetCore.Mvc;

namespace Liopleurodons_Pocket_Business_Helper.Controllers
{
    public class HomeController : Controller
    {
        //This is just a simple controller to return the home page
        //It will be the default page when the user visits the website
        public IActionResult Index()
        {
            return View();
        }
    }
}
