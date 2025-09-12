using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class MonitoringResponseDto
    {
        public MonitoringCardsDto Cards { get; set; } = new MonitoringCardsDto();
        public List<MilestoneChartDto> Graficos { get; set; } = new();
        public List<MilestoneBreakdownDto> Table { get; set; } = new();
        public List<TemporalTrendDto> TendenciaTemporal { get; set; } = new();

        public List<ExecutadoTrendDto> Executado { get; set; } = new();
        public List<PlanejadoExecutadoTrendDto> PlanejadoExecutado { get; set; } = new();

    }
}
