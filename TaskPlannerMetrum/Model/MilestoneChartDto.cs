using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Model
{
    public class MilestoneChartDto
    {
        public int ContractID { get; set; }
        public string MilestoneName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public HoursDto Hours { get; set; } = new HoursDto();
        public costDto Costs { get; set; } = new costDto();

    }
}
