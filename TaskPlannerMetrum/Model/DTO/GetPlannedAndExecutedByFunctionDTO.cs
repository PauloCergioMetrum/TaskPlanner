using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class GetPlannedAndExecutedByFunctionDTO
    {
        public string Period { get; set; }
        public string FunctionName { get; set; }
        public double TotalPlannedHours { get; set; }
        public double TotalExecutedHours { get; set; }
    }
}
