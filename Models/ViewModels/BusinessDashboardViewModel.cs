namespace Liopleurodons_Pocket_Business_Helper.Models.ViewModels
{
    /// <summary>
    /// View model for the Business dashboard (home screen).
    /// Aggregates today's trading summary and recent transactions.
    /// </summary>
    public class BusinessDashboardViewModel
    {
        public string BusinessName { get; set; } = "Thabo's Spaza";
        public string OwnerInitials { get; set; } = "TS";
        public string DayLabel { get; set; } = DateTime.Now.DayOfWeek.ToString();

        // Today's P&L summary
        public decimal TodayRevenue { get; set; }
        public decimal TodayExpenses { get; set; }
        public decimal TodayProfit => TodayRevenue - TodayExpenses;
        public int TodayTransactionCount { get; set; }

        // Recent transactions for ledger list
        public List<TransactionLineViewModel> RecentTransactions { get; set; } = new();

        // Whether to show the "Ready to deposit" banner
        public bool ShowDepositBanner => TodayRevenue > 0;
    }

    public class TransactionLineViewModel
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; }
        public string Emoji { get; set; } = "💰";
        public string TimeLabel { get; set; } = string.Empty;
        public string TypeLabel => IsIncome ? "Sale" : "Expense";
        public string AmountDisplay => (IsIncome ? "+" : "-") + "R" + Amount.ToString("N2");
        public string AmountCssClass => IsIncome ? "inc" : "exp";
        public string IconBackground => IsIncome ? "var(--green-bg)" : "var(--red-bg)";
    }
}
