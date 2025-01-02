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

    

   
}
