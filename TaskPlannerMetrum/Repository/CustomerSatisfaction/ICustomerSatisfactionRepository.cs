using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.CustomerSatisfaction
{
    public interface ICustomerSatisfactionRepository
    {


        public List<CustomerSatisfactionDTO> ClientFeedbackDetailsModel(int contractID);

        public bool CreateCustomerFeedback(    int contractID, string clientResponse, int? clientRating, string receivedComplaint);


    }
}

