using System;
using System.ComponentModel.DataAnnotations;
#nullable enable

namespace TaskPlannerMetrum.Model.DTO
{
    public class GetPlannedAndExecutedByFunctionDTO
    {
        [Key]
        public string? Period { get; set; }
        public string? FunctionName { get; set; }
        public double? TotalPlannedHours { get; set; }
        public double? TotalExecutedHours { get; set; }
    }
}
