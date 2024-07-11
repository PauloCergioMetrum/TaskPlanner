using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class OperationalReportReportDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> ContractIDs { get; set; }
        public List<int> TechLeadIDs { get; set; }
        public List<int> ProjectInspectorIDs { get; set; }
    }
}
