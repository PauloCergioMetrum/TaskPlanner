using System.Collections.Generic;

namespace TaskPlannerMetrum.Model
{
#nullable enable
    public class StatusReportsResult
    {
        public int? TypeID { get; set; }
        public string? TypeDescription { get; set; }
        public double? PlannedTotal { get; set; }
        public List<MadeDetailsStatusResult>? MadeDetails { get; set; }
        public double? TotalPlanned { get; set; }
        public double? TotalMade { get; set; }
    }

}
