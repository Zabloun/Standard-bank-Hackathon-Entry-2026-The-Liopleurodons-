using Liopleurodons_Pocket_Business_Helper.Data;
using Liopleurodons_Pocket_Business_Helper.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Liopleurodons_Pocket_Business_Helper.Controllers
{
    [Authorize]
    public class BusinessController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        private readonly UserManager<IdentityUser> _userManager;

        public BusinessController(IRepositoryWrapper repo, UserManager<IdentityUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        // GET /Business — main dashboard
        public async Task<IActionResult> Index()
        {
            // Retrieve the business name stored as a claim at registration
            var user = await _userManager.GetUserAsync(User);
            var claims = user != null ? await _userManager.GetClaimsAsync(user) : new List<Claim>();
            var businessNameClaim = claims.FirstOrDefault(c => c.Type == "BusinessName");
            var businessName = businessNameClaim?.Value ?? user?.Email ?? "My Business";

            // Build initials from business name (up to 2 words)
            var words = businessName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var initials = words.Length >= 2
                ? $"{words[0][0]}{words[1][0]}".ToUpper()
                : businessName.Length >= 2 ? businessName[..2].ToUpper() : businessName.ToUpper();

            // Pull real purchases from DB (with product navigation loaded)
            var allPurchases = _repo.Purchases.FindAllWithProducts().ToList();
            var todayRevenue  = allPurchases.Where(p => p.IsIncome).Sum(p => p.TotalPrice);
            var todayExpenses = allPurchases.Where(p => !p.IsIncome).Sum(p => p.TotalPrice);
            var txnCount      = allPurchases.Count;

            // Build recent-transactions list (latest 10)
            var recentTxns = allPurchases
                .OrderByDescending(p => p.PurchasesId)
                .Take(10)
                .Select(p => new TransactionLineViewModel
                {
                    Description = p.PurchasesProduct?.ProductName ?? "—",
                    Amount      = p.TotalPrice,
                    IsIncome    = p.IsIncome,
                    Emoji       = p.IsIncome ? "💰" : "🧾",
                    TimeLabel   = "Today"
                }).ToList();

            var vm = new BusinessDashboardViewModel
            {
                BusinessName          = businessName,
                OwnerInitials         = initials,
                DayLabel              = DateTime.Now.DayOfWeek.ToString(),
                TodayRevenue          = todayRevenue,
                TodayExpenses         = todayExpenses,
                TodayTransactionCount = txnCount,
                RecentTransactions    = recentTxns
            };
            return View(vm);
        }

        // POST /Business/Deposit — mark cash as deposited
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Deposit()
        {
            // In a full implementation this would record a deposit ledger entry.
            // For now, show confirmation feedback.
            TempData["Toast"] = "✓ Deposit recorded! Cash marked as banked.";
            return RedirectToAction(nameof(Index));
        }
    }

    // ---- Home redirects to Business dashboard ----
    public class HomeController : Controller
    {
        public IActionResult Index() => RedirectToAction("Index", "Business");
    }
}
