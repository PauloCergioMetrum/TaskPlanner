using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class Satisfaction_Customer
    {
        public int ID { get; set; }


        public int ContractID { get; set; }

        public string ClientResponse { get; set; }

        public int? ClientRating { get; set; }

        public string ReceivedComplaint { get; set; }
    }
}
