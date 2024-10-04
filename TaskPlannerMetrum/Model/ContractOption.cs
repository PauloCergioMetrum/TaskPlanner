using System.Collections.Generic;

namespace TaskPlannerMetrum.Model
{
    public class ContractOption
    {
        public int? ContractID { get; set; }
        public string InternalCode { get; set; }
    }


    public class ContractGraphicRequest
    {
        public List<ContractOption> Contracts { get; set; }
    }
}
