using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.DTO
{
    public class GetTaskExecutionMetricsDTO
    {
        [Key]
        public double? TotalExecutedHours { get; set; }
        public double? TotalPlannedHours { get; set; }
        public int? TotalAvailableHours { get; set; }
    }
}
