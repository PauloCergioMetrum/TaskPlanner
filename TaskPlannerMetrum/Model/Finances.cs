using System;
using System.Security.Cryptography;

namespace TaskPlannerMetrum.Model
{
      #nullable enable
    public class Finances
    {
        public int id { get; set; }
        public string? Description { get; set; }

        public string?   Amount { get; set; }

        public double Value { get; set; }

        public double InvoicedValue { get; set; }

        public DateTime BaseDate { get; set; }

        public DateTime EndDate { get; set; }
        public string? ExpectedInvoiceDate { get; set; }

        public string? Status { get; set; }

        public DateTime InvoicedDate { get; set; }

        public string ?Billing { get; set; }

        public int DepartmentID { get; set; }

        public string?FinanceType { get; set; }

        public int ContractID { get; set; }

        public int? WorkSpaceID { get; set; }


        public string?invoice { get; set; }

        public string? paymentCondition { get; set; }

        public string? BusinessUnit { get; set; }

        public string? StatusDpv { get; set; }
        public bool? Guarantee { get; set; }
        public int? GuaranteePeriod { get; set; }




    }
}
