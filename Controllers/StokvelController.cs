using Liopleurodons_Pocket_Business_Helper.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Liopleurodons_Pocket_Business_Helper.Controllers
{
    [Authorize]
    public class StokvelController : Controller
    {
        // GET /Stokvel
        public IActionResult Index()
        {
            var vm = BuildStokvelVm();
            return View(vm);
        }

        // POST /Stokvel/Contribute
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Contribute(ContributeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["Toast"] = "⚠ Please enter a valid amount.";
                return RedirectToAction(nameof(Index));
            }
            // In a real app, persist to DB and update pot total
            TempData["Toast"] = $"✓ R{vm.Amount:N2} contributed to pot!";
            return RedirectToAction(nameof(Index));
        }

        // POST /Stokvel/InviteMember
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult InviteMember(InviteMemberViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["Toast"] = "⚠ Please enter a valid name and email.";
                return RedirectToAction(nameof(Index));
            }
            // In a real app, send invitation email / create pending user
            TempData["Toast"] = $"✓ Invite sent to {vm.Name}!";
            return RedirectToAction(nameof(Index));
        }

        // ---- Helpers ----
        private static StokvelViewModel BuildStokvelVm() => new()
        {
            GroupName         = "Thabo's Savings Group",
            TotalPot          = 14_500.00m,
            MonthlyContribution = 500.00m,
            CurrentHolder     = "Sipho Khumalo",
            CycleStart        = "Jan 2025",
            YourTurnMonth     = "June 2025",
            YourPosition      = 2,
            TotalMembers      = 5,
            Members = new()
            {
                new() { Initials="SK", Name="Sipho Khumalo",  Month="May 2025",       Position=1, IsCurrent=true,  IsYou=false, AvatarColor="#00A264" },
                new() { Initials="TM", Name="Thabo Mokoena",  Month="June 2025",      Position=2, IsCurrent=false, IsYou=true,  AvatarColor="#1248D4" },
                new() { Initials="LN", Name="Lerato Nkosi",   Month="July 2025",      Position=3, IsCurrent=false, IsYou=false, AvatarColor="#7B3F00" },
                new() { Initials="PM", Name="Palesa Molefe",  Month="August 2025",    Position=4, IsCurrent=false, IsYou=false, AvatarColor="#5C0F8B" },
                new() { Initials="ND", Name="Nkosi Dlamini",  Month="September 2025", Position=5, IsCurrent=false, IsYou=false, AvatarColor="#8B2500" },
            },
            RecentContributions = new()
            {
                new() { MemberName="Sipho Khumalo", Initials="SK", Amount=500, DateLabel="Today",       Paid=true,  AvatarColor="#00A264" },
                new() { MemberName="Thabo Mokoena", Initials="TM", Amount=500, DateLabel="Today",       Paid=true,  AvatarColor="#1248D4" },
                new() { MemberName="Lerato Nkosi",  Initials="LN", Amount=500, DateLabel="Yesterday",   Paid=true,  AvatarColor="#7B3F00" },
                new() { MemberName="Palesa Molefe", Initials="PM", Amount=500, DateLabel="3 days ago",  Paid=false, AvatarColor="#5C0F8B" },
                new() { MemberName="Nkosi Dlamini", Initials="ND", Amount=500, DateLabel="—",           Paid=false, AvatarColor="#8B2500" },
            }
        };
    }
}
