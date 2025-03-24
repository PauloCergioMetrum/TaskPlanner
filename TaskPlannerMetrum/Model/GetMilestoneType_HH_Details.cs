#nullable enable
using System;


namespace TaskPlannerMetrum.Model
{
    public class GetMilestoneType_HH_Details
    {
        public string MilestoneTypeID { get; set; }  
        public string? DepartmentName { get; set; }
        public string? DisplacementServiceName { get; set; }
        public int DepartmentID { get; set; }
        public string MilestonesValueID { get; set; }
        public string? FunctionName { get; set; }
       public double CostHoursExpected { get; set; }   
        public string? DetailsHH { get; set; }
        public int ContractID { get; set; }
         public int? MilestonesID { get; set; }
    }
}



