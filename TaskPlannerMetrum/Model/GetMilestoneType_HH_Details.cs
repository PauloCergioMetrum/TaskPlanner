 using System;

namespace TaskPlannerMetrum.Model
{
    public class GetMilestoneType_HH_Details
    {
        public string MilestoneTypeID{ get; set; }
        public string DepartmentName { get; set; }
        public string DisplacementServiceName { get; set; }
        public int DepartmentID { get; set; }
        public string MilestonesValueID { get; set; }
        public int DisplacementServicesID { get; set; }
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public double ValueHour { get; set; } // Changed from decimal to double
        public double HoursExpected { get; set; } // Changed from decimal to double
        public double HoursPlanned { get; set; } // Changed from decimal to double
        public double HoursExecuted { get; set; } // Changed from decimal to double
        public double CostHoursExpected { get; set; } // Changed from decimal to double
        public double CostHoursPlanned { get; set; } // Changed from decimal to double
        public double CostHoursExecuted { get; set; } // Changed from decimal to double
    }
}
