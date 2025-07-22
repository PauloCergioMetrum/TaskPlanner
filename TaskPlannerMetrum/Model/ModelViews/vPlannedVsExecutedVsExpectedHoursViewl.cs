using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

[Keyless]
[Table("vPlannedVsExecutedVsExpectedHours")]
public class PlannedVsExecutedVsExpectedHoursViewModel
{
    public int ContractID { get; set; }
    public string InternalCode { get; set; }
    public double TotalExecutedCost { get; set; }
    public double TotalExecutedHours { get; set; }
    public double TotalPlannedHours { get; set; }
    public double TotalExpectedHours { get; set; }
}
