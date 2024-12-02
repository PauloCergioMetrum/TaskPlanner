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
        public double? PlannedTotal { get; set; }
        public double? MadeTotal { get; set; }
    }

    public class vStatusReportsGraph
    {
        public int? TypeAcquisitionID { get; set; }
        public string? TypeAcquisitionDescription { get; set; }
        public int? AmountPlanned { get; set; }
        public double? PredictedTotal { get; set; }
        public double? TotalCost { get; set; }
        public double? AmountValue { get; set; }
        public string? AcquisitionDescription { get; set; }
        public int? ContractID { get; set; }
        public double? Amount { get; set; }
        public string? AquisitionPlannedID { get; set; }
        public double? Value { get; set; }
        public DateTime? DateAcquisition { get; set; }
        public DateTime? DateAcquisitionDelivery { get; set; }
        public double? TotalMade { get; set; }
    }

    public class vhhGraphicDetail
    {
        public int? MilestoneItemID { get; set; }
        public int? ContractID { get; set; }
        public string? MilestoneValueID { get; set; }
        public int? MilestonesID { get; set; }
        public string? DisplacementServiceName { get; set; }
        public int? DisplacementServicesID { get; set; }
        public double? ValueHour { get; set; }
        public double? HoursExpected { get; set; }
        public double? HoursPlanned { get; set; }
        public double? HoursExecuted { get; set; }
        public double? CostHoursExpected { get; set; }
        public double? CostHoursPlanned { get; set; }
        public double? CostHoursExecuted { get; set; }
    }
}
