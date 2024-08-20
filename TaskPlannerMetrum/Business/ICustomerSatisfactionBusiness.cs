using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business
{
    public interface ICustomerSatisfactionBusiness
    {
        List<CustomerSatisfactionDTO> ClientFeedbackDetailsModel(int contractID);
        public bool CreateCustomerFeedback( DateTime FeedbackDate, int contractID, string clientResponse, int? clientRating, string receivedComplaint);
    }
}
