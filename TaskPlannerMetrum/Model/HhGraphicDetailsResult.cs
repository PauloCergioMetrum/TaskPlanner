using System.Collections.Generic;

namespace TaskPlannerMetrum.Model
{
    public class HhGraphicDetailsResult
    {
        public int ContractID { get; set; }
        public List<HhGraphicDetailItemResult>? Details { get; set; }
    }
}
