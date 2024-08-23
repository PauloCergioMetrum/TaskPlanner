using DocumentFormat.OpenXml.Office.CoverPageProps;
using System;
namespace TaskPlannerMetrum.Model
{
    public class CustomerSastifaction
    {
        public long ID { get; set; }
        public string ContractName { get; set; }
        public string Contact { get; set; }
        public DateTime? LastExecutedDate { get; set; }
        public DateTime? LastContactDate { get; set; }
        public DateTime? FeedbackDate { get; set; }
         public string ClientName { get; set; }
        public int ClientRating { get; set; }
        public string ReceivedComplaint { get; set; }
        public int StatusID { get; set; }
        public string BusinessUnit { get; set; }  
        public string VendorName { get; set; }
        public string InspectorName{ get; set; }


}
}
