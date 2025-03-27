using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class OperationalReportReportDTO
    {

        public string StartPeriod { get; set; }
        public string EndPeriod { get; set; }
        public List<int> ContractIDs { get; set; }
        public List<int> TechLeadIDs { get; set; }
        public List<int> ProjectInspectorIDs { get; set; }
        public List<int> BusinessUnitIDs { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public string StatusFilter { get; set; }

        public List<int> InspectorIDs { get; set; }

     




    }

    public class PreparetBalancePerProject
    {
        public string BusinessUnit { get; set; }

        public List<BalancePerProject> Details { get; set; } 
    }
}
