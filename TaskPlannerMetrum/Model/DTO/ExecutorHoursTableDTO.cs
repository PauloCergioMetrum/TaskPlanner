namespace TaskPlannerMetrum.Model.DTO
{
    public class ExecutorHoursTableDTO
    {
        public string? Executor { get; set; }
        public string? ExecutorFunction { get; set; }
        public string? Department { get; set; }
        public double? PlannedHours { get; set; }
        public double? ExecutedHours { get; set; }
        public int? AvailableHours { get; set; }

     
    }
}
