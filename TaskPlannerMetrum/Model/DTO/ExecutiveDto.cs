using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ExecutiveDto
    {
        public int InspectorId { get; set; }
        public string InspectorName { get; set; }
        public int CompanyId { get; set; }
        public string WorkspaceName { get; set; }
        public string BusinessUnit { get; set; }
        public string SalesOrder { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string SalesDescription { get; set; }
        public double ItemValue { get; set; }
        public double InvoicedValue { get; set; }
        public DateTime? ExpectedInvoiceMonth { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public string Status { get; set; }
    }
}
