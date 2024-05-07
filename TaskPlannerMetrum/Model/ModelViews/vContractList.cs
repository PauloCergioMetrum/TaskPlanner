using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vContractList
    {

        [Key]
        public int ContractID { get; set; }
        public string ClientName { get; set; }
        public string PaymentMethod { get; set; }
        public string Condition { get; set; }
        public string InternalCode { get; set; }
        public DateTime StartDate { get; set; }
        public string InspectorName { get; set; }
       
        public string VendorName { get; set; }

        public Int64 ClientOrder { get; set; }

        public bool EnableProject { get; set; }

        public double ValueTotal { get; set; }

        public double InvoicedValueTotal { get; set; }

        public string BusinessUnit { get; set; }

        public string Observation { get; set; }

        public DateTime? DateRetroactive {  get; set; }

        public double? PredictedSavings { get; set; }

        public double ?PredictedMarkup { get; set; }

        public DateTime? ValidityStartDate { get; set; }

        public DateTime? ValidityEndDate { get; set; }

        public string Status { get; set; }
        public int StatusID { get; set; }  
        
        public string StatusGuarantee {  get; set; }

        public string? PaymentCondition { get; set; }

        public int WorkSpaceID { get; set; }
    }
}
