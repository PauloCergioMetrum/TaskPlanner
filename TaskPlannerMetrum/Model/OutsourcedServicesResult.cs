using System.Collections.Generic;

namespace TaskPlannerMetrum.Model
{
    public class OutsourcedServicesResult
    {
        public int ContractID { get; set; }
        public double? TotalOutsourcedServicesPlannedSum { get; set; }
        public double? TotalOutsourcedServicesMadeSum { get; set; }
        public List<OutsourcedServiceDetailsResult>? Services { get; set; }
    }

}
