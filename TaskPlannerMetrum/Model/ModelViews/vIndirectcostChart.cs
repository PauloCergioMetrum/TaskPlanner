using System;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vIndirectcostChart
    {
        public int? ContractID { get; set; }
        public string? PlannedId { get; set; }
        public string? MadeId { get; set; }
        public int? TypeID { get; set; }
        public string? TypeDescription { get; set; }
        public double? TotalPlanned { get; set; }
        public double? MadeTotal { get; set; }
    }

    public class vAcquisitionChart
    {
        public string ID { get; set; }
        public int TypeAcquisitionID { get; set; }
        public string TypeAcquisitionDescription { get; set; }
        public double PredictedTotal { get; set; } 
        public double TotalCost { get; set; }
        public double? CombinedTotal { get; set; } 
        public int ContractID { get; set; }

    }

    public class vhhGraphicDetail
    {
        public string MilestoneTypeID { get; set; }  
        public string DepartmentName { get; set; }
        public string DisplacementServiceName { get; set; }
        public int DepartmentID { get; set; }
        public string MilestonesValueID { get; set; }
        public int MilestonesID { get; set; }
        public int ContractID { get; set; }
        public int DisplacementServicesID { get; set; }
        public int FunctionID { get; set; }
        public string FunctionName { get; set; } 
        public double ValueHour { get; set; }
        public double HoursExpected { get; set; }
        public double HoursPlanned { get; set; }         
        public double HoursExecuted { get; set; }
        public double CostHoursExpected { get; set; }
        public double CostHoursPlanned { get; set; }
        public double CostHoursExecuted { get; set; }
        public int? ScheduledDate { get; set; }
    }
}
