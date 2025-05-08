using System;
using System.Collections.Generic;

public class OperationalReportFilterDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<string> InternalCodes { get; set; }
    public List<string> BusinessUnits { get; set; }
    public List<string> ProjectManagers { get; set; }
    public List<string> TechLeaders { get; set; }
    public string ProjectStatus { get; set; } // Pode vir em CSV já, ou ser convertido
    public int? FiltroStatusID { get; set; }
}

