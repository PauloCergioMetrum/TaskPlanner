using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class GetTaskExecutionMetricsDTO
    {
        public double? TotalExecutedHours { get; set; }
        public double? TotalPlannedHours { get; set; }
        public int? TotalAvailableHours { get; set; }
    }
}
