using System;

namespace TaskPlannerMetrum.Model
{
    public class GetMilestoneType_HH_Details
    {
        public string MilestoneTypeID { get; set; }
        public string DepartmentName { get; set; }
        public string DisplacementServiceName { get; set; }
        public int? DepartmentID { get; set; }
        public string MilestonesValueID { get; set; }
        public int? DisplacementServicesID { get; set; }
        public int? FunctionID { get; set; }
        public double? ValueHour { get; set; }
        public double? HoursExpected { get; set; }


        public double? CostHoursExpected { get; set; }
        public double? CostHoursPlanned { get; set; }
        public double? CostHoursExecuted { get; set; }



        public string? FunctionName { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public string? DetailsHH { get; set; }
    }
}
