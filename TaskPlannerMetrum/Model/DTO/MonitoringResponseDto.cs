using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Model.DTO
{
    public class MonitoringResponseDto
    {
        public MonitoringCardsDto Cards { get; set; } = new MonitoringCardsDto();
        public List<MilestoneChartDto> Graphic { get; set; } = new();
        public List<MilestoneBreakdownDto> Table { get; set; } = new();
        public List<vw_temporal_trend> TemporalTrend { get; set; } = new();

        public List<ExecutedTrendDto> Executed { get; set; } = new();
        public List<PlannedVsExecutedTrendDto> PlannedVsExecuted { get; set; } = new();
        public ForecastDto Forecast { get; set; } = new();
        public double? TotalForecastHours { get; set; }


    }
}
