namespace TaskPlannerMetrum.Model.DTO
#nullable enable

{
    public class TaskExecutionMetricsDTO
    {
        public string? FunctionName { get; set; }
        public double? TotalExecutedHours { get; set; }
        public double? TotalPlannedHours { get; set; }
        public double? TotalAvailableHours { get; set; }
    }
}
