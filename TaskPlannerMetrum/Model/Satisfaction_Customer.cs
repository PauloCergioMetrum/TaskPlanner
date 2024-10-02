using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class Satisfaction_Customer
    {
        public int ID { get; set; }

        public DateTime? FeedbackDate {  get; set; }     
        public int ContractID { get; set; }

        public string ClientResponse { get; set; }

        public int? ClientRating { get; set; }

        public string ReceivedComplaint { get; set; }

        public int? status { get; set; }
    }
}
