using System.ComponentModel.DataAnnotations;

namespace Liopleurodons_Pocket_Business_Helper.Models.ViewModels
{
    public class StokvelMember
    {
        public string Initials { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Month { get; set; } = string.Empty;
        public int Position { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsYou { get; set; }
        public string AvatarColor { get; set; } = "#00A264";
    }

    public class StokvelContribution
    {
        public string MemberName { get; set; } = string.Empty;
        public string Initials { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string DateLabel { get; set; } = string.Empty;
        public bool Paid { get; set; }
        public string AvatarColor { get; set; } = "#00A264";
    }

    public class StokvelViewModel
    {
        public string GroupName { get; set; } = "Thabo's Savings Group";
        public decimal TotalPot { get; set; }
        public decimal MonthlyContribution { get; set; }
        public string CurrentHolder { get; set; } = string.Empty;
        public string CycleStart { get; set; } = string.Empty;
        public string YourTurnMonth { get; set; } = string.Empty;
        public int YourPosition { get; set; }
        public int TotalMembers { get; set; }
        public List<StokvelMember> Members { get; set; } = new();
        public List<StokvelContribution> RecentContributions { get; set; } = new();
    }

    public class ContributeViewModel
    {
        [Required]
        [Range(1, 100000, ErrorMessage = "Amount must be greater than 0.")]
        [Display(Name = "Amount (R)")]
        public decimal Amount { get; set; }

        [Display(Name = "Note (optional)")]
        [StringLength(200)]
        public string? Note { get; set; }
    }

    public class InviteMemberViewModel
    {
        [Required, Display(Name = "Full Name")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
