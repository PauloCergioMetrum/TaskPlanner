using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ClientFeedbackDetailsView
    {
        public long ID { get; set; }
        public string ContractName { get; set; }
        public string Contact { get; set; }
        public string Scope { get; set; }
        public DateTime? LatestScheduledDate { get; set; }
        public DateTime? LastContactDate { get; set; }
        public string ClientResponse { get; set; }
        public int ClientRating { get; set; }
        public string ReceivedComplaint { get; set; }
        public int ContractID { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public string MaxScheduledDate { get; set; }


    }


}

