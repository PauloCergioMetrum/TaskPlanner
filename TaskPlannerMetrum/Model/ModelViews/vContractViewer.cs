using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vContractViewer
    {
        [Key]
        public int financeid { get; set; }

        public int cID { get; set; }

        public string Owner { get; set; }

        public string Company { get; set; }

        public DateTime StartDate { get; set; }

        public string SeniorCode { get; set; }

        public string ClientName { get; set; }

        public string Description { get; set; }

        public string Vendor { get; set; }

        [Column("Amount.")]
        public string Amount { get; set; }

        public double TotalValue { get; set; }


        public double InvoicedValue { get; set; }

        public DateTime BaseDate { get; set; }

        public DateTime EndDate { get; set; }

        public string BillingMonth { get; set; }

        public string PaymentMethod { get; set; }

        public string paymentCondition { get; set; }

        public DateTime InvoicedDate { get; set; }

        public string invoice { get; set; }

        public string DepartmentName { get; set; }

        public string FinanceType { get; set; }

        public string PVStatus { get; set; }

        public string PaymentStatus { get; set; }

    





    }
}
