using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class CustomerSatisfactionDTO
    {
        public long ID { get; set; }
        public string ContractName { get; set; }
        public string Contact { get; set; }
        public string Scope { get; set; }
        public DateTime? LastExecutedDate { get; set; }
        public DateTime? LastContactDate { get; set; }
        public string ClientResponse { get; set; }
        public int ClientRating { get; set; }
        public string ReceivedComplaint { get; set; }
    }

}

