using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vw_FinancialProgressSCurve
    {
        [Key]
        public int ContractID { get; set; }
        public string? ContractName { get; set; }
        public string? DateFormatted { get; set; }
        public string? Status { get; set; }
        public double? ExecutedCost { get; set; }
        public double? PlannedCost { get; set; }
        public double? PlannedRevenue { get; set; }
        public double? RealizedRevenue { get; set; }
        public double? Balance { get; set; }
        public string BreakEvenDate { get; set; } = string.Empty;
        public int? DaysInNegative { get; set; }
    }
}
