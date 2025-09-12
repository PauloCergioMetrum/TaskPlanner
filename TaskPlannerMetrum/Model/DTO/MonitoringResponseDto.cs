using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class MonitoringResponseDto
    {
        public MonitoringCardsDto Cards { get; set; } = new MonitoringCardsDto();
        public List<MilestoneChartDto> Graficos { get; set; } = new();
        public List<MilestoneBreakdownDto> Table { get; set; } = new();
        public List<TemporalTrendDto> TemporalTrend { get; set; } = new();

        public List<ExecutedTrendDto> Executed { get; set; } = new();
        public List<PlannedVsExecutedTrendDto> PlannedVsExecuted { get; set; } = new();


    }
}
