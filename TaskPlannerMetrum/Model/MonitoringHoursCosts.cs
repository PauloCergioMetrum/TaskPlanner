using Microsoft.EntityFrameworkCore;

[Keyless]
public class MonitoringHoursCosts
{
    public int ContractID { get; set; }

    // Hours (HH)
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
