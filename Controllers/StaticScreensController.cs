using Microsoft.AspNetCore.Mvc;

namespace Liopleurodons_Pocket_Business_Helper.Controllers
{
    /// <summary>ATM Finder screen — static view, map integration is future work.</summary>
    public class ATMController : Controller
    {
        public IActionResult Index() => View();
    }

    /// <summary>Personal banking overview screen.</summary>
    public class PersonalController : Controller
    {
        public IActionResult Index() => View();
    }

    /// <summary>Stokvel / savings group screen.</summary>
    public class StokvelController : Controller
    {
        public IActionResult Index() => View();
    }
}
