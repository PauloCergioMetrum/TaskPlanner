using Microsoft.EntityFrameworkCore;

[Keyless]
public class TemporalTrend
{
    public int ContractID { get; set; }
    public string MonthYear { get; set; } = string.Empty;
    public double? TotalPlannedHours { get; set; }
    public double? TotalExecutedHours { get; set; }
    public double? TotalHourCost { get; set; }
}
