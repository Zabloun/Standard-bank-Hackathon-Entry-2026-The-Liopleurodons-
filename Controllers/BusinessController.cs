using Liopleurodons_Pocket_Business_Helper.Data;
using Liopleurodons_Pocket_Business_Helper.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Liopleurodons_Pocket_Business_Helper.Controllers
{
    [Authorize]
    public class BusinessController : Controller
    {
        private readonly IRepositoryWrapper _repo;

        public BusinessController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // GET /Business — main dashboard
        public IActionResult Index()
        {
            // TODO: pull real P&L from _repo; seeded sample data used here
            var vm = new BusinessDashboardViewModel
            {
                BusinessName          = "Thabo's Spaza",
                OwnerInitials         = "TS",
                DayLabel              = DateTime.Now.DayOfWeek.ToString(),
                TodayRevenue          = 190.00m,
                TodayExpenses         = 380.00m,
                TodayTransactionCount = 4,
                RecentTransactions    = new()
                {
                    new() { Description = "Bread & butter",  Amount = 45,  IsIncome = true,  Emoji = "🍞", TimeLabel = "08:15" },
                    new() { Description = "2L Coca-Cola",    Amount = 25,  IsIncome = true,  Emoji = "🥤", TimeLabel = "09:02" },
                    new() { Description = "Airtime bundles", Amount = 120, IsIncome = true,  Emoji = "📦", TimeLabel = "09:47" },
                    new() { Description = "Stock restock",   Amount = 380, IsIncome = false, Emoji = "🧾", TimeLabel = "07:30" },
                }
            };
            return View(vm);
        }
    }

    // ---- Home redirects to Business dashboard ----
    public class HomeController : Controller
    {
        public IActionResult Index() => RedirectToAction("Index", "Business");
    }
}
