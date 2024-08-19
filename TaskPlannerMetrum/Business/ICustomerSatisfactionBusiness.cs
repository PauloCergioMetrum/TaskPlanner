using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business
{
    public interface ICustomerSatisfactionBusiness
    {
        List<CustomerSatisfactionDTO> ClientFeedbackDetailsModel(int contractID);
    }
}
