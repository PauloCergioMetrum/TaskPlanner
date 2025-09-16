using System.Collections.Generic;
using TaskPlannerMetrum.Model;

public sealed class MonitoringCardsDto
{
    public int ContractID { get; set; } 
    public decimal ExpectedHours { get; set; } 
    public double PlannedHours { get; set; }     
    public double ExecutedHours { get; set; }    
    public double? HoursDifference { get; set; }

    // Costs (R$)
    public decimal ExpectedCost { get; set; }    
    public decimal PlannedCost { get; set; }     
    public decimal ExecutedCost { get; set; }    
    public decimal? CostDifference { get; set; }


}
