using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.CustomerSatisfaction
{
    public interface ICustomerSatisfactionRepository
    {


        public List<CustomerSatisfactionDTO> ClientFeedbackDetailsModel(int contractID);


    }
}

