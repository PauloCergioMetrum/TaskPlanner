using System;
using System.Collections.Generic;

public class OperationalReportFilterDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<string> InternalCodes { get; set; } = new(); 
    public List<string> BusinessUnits { get; set; } = new();
    public List<string> ProjectManagers { get; set; } = new();
    public List<string> TechLeaders { get; set; } = new();
    public string ProjectStatus { get; set; }
    public int? FiltroStatusID { get; set; }
}
