using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model
{
    public class ReportInvoiceDetails
    {

        public List<PredictedInvoiced> PredictedInvoiced{ get; set; }

        public List<MaterialServices> MaterialAndService { get; set; }
    }
}
