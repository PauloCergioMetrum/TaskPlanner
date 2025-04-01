using System;

namespace TaskPlannerMetrum.Model
{
 


        public class OrderManagementInfo
        {
            public int ContractID { get; set; }
            public double? TotalCostHoursPlanned { get; set; }
            public double? TotalCostHoursExecuted { get; set; }
            public double? TotalCostDifference { get; set; }
            public DateTime? ValidityStartDate { get; set; }
            public DateTime? ValidityEndDate { get; set; }
            public double? TotalCostExpected { get; set; }
 
            public double? TotalCostExecuted { get; set; }
            public double? ManagementGrandTotal { get; set; }
        }

    public class OrderManagementResult
    {
        public int? ContractID { get; set; }
        public float? TotalDifferenceExpectedExecuted { get; set; }
        public float? TotalCostHoursExecuted { get; set; }
        public decimal? ManagementGrandTotal { get; set; }
        public decimal? TotalCostExpected { get; set; }

    }




}