namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vw_temporal_trend
    {
        public int ContractID { get; set; }
        public string Period { get; set; } = string.Empty;

        public double PlannedHoursBaseline { get; set; }
        public double PlannedCostBaseline { get; set; }

        public double ScheduledHours { get; set; }
        public double ScheduledCost { get; set; }

        public double ActualHours { get; set; }
        public double ActualCost { get; set; }


        public double TotalBaselineHours { get; set; }
        public double TotalBaselineCost { get; set; }
    }
}
