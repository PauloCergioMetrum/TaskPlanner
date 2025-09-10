using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class MonitoringResponseDto
    {
        public MonitoringCardsDto Cards { get; set; } = new MonitoringCardsDto();
        public List<MilestoneChartDto> Graficos { get; set; } = new();
    }
}
