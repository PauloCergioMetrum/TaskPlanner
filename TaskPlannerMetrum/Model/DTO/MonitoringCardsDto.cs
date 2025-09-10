using System.Collections.Generic;
using TaskPlannerMetrum.Model;

public sealed class MonitoringCardsDto
{
    public int ContractID { get; init; }

    // HH
    public decimal PrevistoHH { get; init; }   // decimal(38,2)
    public double PlanejadoHH { get; init; }  // float -> double
    public double ExecutadoHH { get; init; }  // float -> double
    public double? DiferencaHH { get; init; }  // float, null

    // R$
    public decimal PrevistoRS { get; init; }   // decimal(38,2)
    public decimal PlanejadoRS { get; init; }  // decimal(38,2)
    public decimal ExecutadoRS { get; init; }  // decimal(38,2)
    public decimal? DiferencaRS { get; init; } // decimal(38,2), null

    public List<MilestoneChartDto> Graficos { get; set; } = new();
}
