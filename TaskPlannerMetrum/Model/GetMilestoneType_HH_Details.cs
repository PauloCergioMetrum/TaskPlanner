 using System;

namespace TaskPlannerMetrum.Model
{
    public class GetMilestoneType_HH_Details
    {
        public string MilestoneTypeID{ get; set; }
        public string DepartmentName { get; set; }
        public string DisplacementServiceName { get; set; }
        public int DepartmentID { get; set; }
        public int MilestonesValueID { get; set; }
        public int DisplacementServicesID { get; set; }
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public  int ValueHour { get; set; }
        public int HoursExpected { get; set; }

        public int HoursPlanned { get; set; }
        public int HoursExecuted { get; set; }
        public int CostHoursExpected { get; set; }
        public int CostHoursPlanned { get; set; }
        public int CostHoursExecuted { get; set; }
    }
}
