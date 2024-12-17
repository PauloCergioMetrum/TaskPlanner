using System.Collections.Generic;

namespace TaskPlannerMetrum.Model
{
    public class IndirectCostsResult
    {
        public int? ContractID { get; set; }
        public string? PlannedId { get; set; }
        public int? TypeID { get; set; }
        public string? TypeDescription { get; set; }
        public double? PlannedTotal { get; set; }
        public List<MadeDetailsResult>? MadeDetails { get; set; }
    }
}
