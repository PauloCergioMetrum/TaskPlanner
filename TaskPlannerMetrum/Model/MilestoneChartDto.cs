using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Model
{
    public class MilestoneChartDto
    {
        public int ContractID { get; set; }
        public string Marco { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public HoursDto Horas { get; set; } = new HoursDto();
        public costDto Custos { get; set; } = new costDto();
    }
}
