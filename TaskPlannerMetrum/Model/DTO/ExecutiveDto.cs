using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ExecutiveDtoAll
    {
        public List<ExecutiveDto> ExecutiveDtoList { get; set; }
        public List<GetPvSummary> GetPvSummaryList { get; set; }
    }


    public class ExecutiveDto
    {
        public long ID { get; set; }
        public int InspectorID { get; set; }
        public string UserName { get; set; }
        public int WorkSpaceID { get; set; }
        public string Name { get; set; }
        public string BusinessUnit { get; set; }
        public string InternalCode { get; set; }
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string Condition { get; set; }
        public double Value { get; set; }
        public double InvoicedValue { get; set; }
        public string ExpectedInvoiceDate { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public string StatusDpv { get; set; }


    }

    public class GetPvSummary
    {
        public int Abertos { get; set; }
        public int Fechados  { get; set; }
    }
}
