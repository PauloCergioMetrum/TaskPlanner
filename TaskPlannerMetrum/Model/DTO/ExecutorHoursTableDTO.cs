using System.ComponentModel.DataAnnotations;
#nullable enable
namespace TaskPlannerMetrum.Model.DTO
{
    public class ExecutorHoursTableDTO
    {
        [Key]
        public string? Executor { get; set; }
        public string? Department { get; set; }
        public double? PlannedHours { get; set; }
        public double? ExecutedHours { get; set; }
       public double? AvailableHours { get; set; }
        public string? ExecutorFunction { get; set; }

    }
}





