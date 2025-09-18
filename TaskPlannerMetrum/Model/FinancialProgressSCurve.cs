namespace TaskPlannerMetrum.Model
{
    public class FinancialProgressSCurve
    {
        public int ContractID { get; set; }
        public string? ContractName { get; set; }
        public string? DateFormatted { get; set; }
        public string? Status { get; set; }
        public float? ExecutedCost { get; set; }
        public float? PlannedCost { get; set; }
        public float? PlannedRevenue { get; set; }
        public float? RealizedRevenue { get; set; }
        public float? Balance { get; set; }
        public string BreakEvenDate { get; set; } = string.Empty;
        public int? DaysInNegative { get; set; }
    }
}
