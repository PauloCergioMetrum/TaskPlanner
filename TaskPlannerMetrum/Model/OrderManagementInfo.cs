using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
 


        public class OrderManagementInfo
        {
        [Key]
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
        public double? TotalDifferenceExpectedExecuted { get; set; }
        public double? TotalCostHoursExecuted { get; set; }
        public double? ManagementGrandTotal { get; set; }
        public double? TotalCostExpected { get; set; }

    }




}