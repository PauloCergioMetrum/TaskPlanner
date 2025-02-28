using System;

namespace TaskPlannerMetrum.Model
{
    public class OrderManagementInfo
    {



        public int ContractID { get; set; }
         
        public double? TotalCostHoursPlanned { get; set; }

        public double? TotalCostHoursExecuted { get; set; }
        public double? TotalCostDifference { get; set; }
        public DateTime? ValidityStartDate {  get; set; }
        public DateTime? ValidityEndDate { get; set; }
        public double? TotalCostExpected { get; set; }
        public double? TotalCostExecuted { get; set; }

    }

   

}
