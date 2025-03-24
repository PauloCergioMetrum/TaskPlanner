namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vRightCardValue
    {
        public int? ContractID { get; set; }
        public decimal? TotalValue { get; set; }
        public decimal? TotalInvoicedValue { get; set; }
        public decimal? TotalCostDifference { get; set; }
        public decimal? TotalCostHoursExecuted { get; set; }
        public decimal? TotalGrossResult { get; set; }
        public string PredictedMarkup { get; set; }
        public decimal? TotalCostHoursExecutedPercentage { get; set; }
    }
}
