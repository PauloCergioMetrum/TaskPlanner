using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model
{
    public class ReportInvoice
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<string> BusinessUnits { get; set; } = new List<string>();

        public List<int> InspectorIDs { get; set; } = new List<int>();
    }
}
