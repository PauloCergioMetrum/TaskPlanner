using System;

namespace TaskPlannerMetrum.Model.DTO
{
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
}
