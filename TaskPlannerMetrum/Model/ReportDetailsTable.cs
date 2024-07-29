using System;

namespace TaskPlannerMetrum.Model
{
    public class ReportDetailsTable
    {
        public string Inspector { get; set; }         
        public string Company { get; set; }           
        public string BusinessUnit { get; set; }      
        public string InternalCode { get; set; }      
        public string Client { get; set; }            
        public string SaleDescription { get; set; }  
        public double ItemValue { get; set; }      
        public double InvoicedValue { get; set; }   
        public string PredictedInvoicingMonth { get; set; } 
        public DateTime? InvoicingDate { get; set; } 
        public string SalesOrderStatus { get; set; }  
    }
}
