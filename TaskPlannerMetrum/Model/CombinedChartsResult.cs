using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Model
{
    public class CombinedChartsResult
    {
        public List<IndirectCostsResult>? IndirectCosts { get; set; }
        public List<MobilizationResult>? Mobilization { get; set; }
        public List<StatusReportsResult>? StatusReports { get; set; }
        public List<OrderManagementResult>? OrderManagement { get; set; }
        public List<OutsourcedServicesResult>? OutsourcedServices { get; set; }
        public List<HhGraphicDetailsResult>? HhGraphicDetails { get; set; }
    }

    public class IndirectCostsResult
    {
        public int? ContractID { get; set; }
        public string? PlannedId { get; set; }
        public int? TypeID { get; set; }
        public string? TypeDescription { get; set; }
        public double? PlannedTotal { get; set; }
        public List<MadeDetailsResult>? MadeDetails { get; set; }
    }

    public class MadeDetailsResult
    {
        public vIndirectcostChart? AcquisitionMadeDTO { get; set; }
        public double? MadeTotal { get; set; }
    }

    public class MobilizationResult
    {
        public double? AccommodationPlanned { get; set; }
        public double? FoodPlanned { get; set; }
        public double? AirTransportPlanned { get; set; }
        public double? GroundTransportPlanned { get; set; }
        public double? OthersPlanned { get; set; }
        public MobilizationMadeResult? MobilizationMade { get; set; }
    }

    public class MobilizationMadeResult
    {
        public double? AccommodationActual { get; set; }
        public double? FoodActual { get; set; }
        public double? AirTransportActual { get; set; }
        public double? GroundTransportActual { get; set; }
        public double? OthersActual { get; set; }
    }

    public class StatusReportsResult
    {
        public int? TypeID { get; set; }
        public string? TypeDescription { get; set; }
        public double? PlannedTotal { get; set; }
        public List<MadeDetailsStatusResult>? MadeDetails { get; set; }
        public double? TotalPlanned { get; set; }
        public double? TotalMade { get; set; }
    }

    public class MadeDetailsStatusResult
    {
        public int? MadeId { get; set; }
        public double? MadeTotal { get; set; }
    }

    public class OrderManagementResult
    {
        public int ContractID { get; set; }
        public float? TotalDifferenceExpectedExecuted { get; set; }
        public float? TotalCostHoursExecuted { get; set; }
    }

    public class OutsourcedServicesResult
    {
        public int ContractID { get; set; } 
        public double? TotalOutsourcedServicesPlannedSum { get; set; }
        public double? TotalOutsourcedServicesMadeSum { get; set; }
        public List<OutsourcedServiceDetailsResult>? Services { get; set; }
    }

    public class OutsourcedServiceDetailsResult
    {
        public string? ID { get; set; }
        public double? ValueUnit { get; set; }
        public double? Amount { get; set; }
        public double? TypeID { get; set; }
        public string? Description { get; set; }
        public string? Subcontracting { get; set; }
        public double? TotalOutsourcedServices_Planned { get; set; }
        public double? TotalOutsourcedServices_Made { get; set; }
        public double? Difference { get; set; }
    }

    public class HhGraphicDetailsResult
    {
        public int ContractID { get; set; } 
        public List<HhGraphicDetailItemResult>? Details { get; set; }
    }

    public class HhGraphicDetailItemResult
    {
        public string? MilestoneTypeID { get; set; }
        public string? MilestonesValueID { get; set; }
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
