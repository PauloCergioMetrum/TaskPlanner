using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ClientFeedbackDetailsView
    {
#nullable enable
        public long ID { get; set; }
        public string ContractName { get; set; } = string.Empty;
        public string? Scope { get; set; }
        public DateTime? LatestScheduledDate { get; set; }
        public DateTime? LastContactDate { get; set; }
        public string? ClientResponse { get; set; }
        public int ClientRating { get; set; }
        public string? ReceivedComplaint { get; set; }
        public int ContractID { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public DateTime? MaxScheduledDate { get; set; }
        public DateTime? EndofActivities { get; set; }
    
        public string? Name { get; set; }
        public string ?Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public int? Type { get; set; }



    }


}

