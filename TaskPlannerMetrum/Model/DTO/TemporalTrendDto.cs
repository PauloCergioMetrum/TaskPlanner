namespace TaskPlannerMetrum.Model.DTO
{
    public class TemporalTrendDto
    {
        public int ContractID { get; set; }
        public string MonthYear { get; set; } = string.Empty;
        public double? TotalPlannedHours { get; set; }
        public double? TotalExecutedHours { get; set; }
        public double? TotalHourCost { get; set; }
        public double? TotalForecastHours { get; set; }
    }
}
