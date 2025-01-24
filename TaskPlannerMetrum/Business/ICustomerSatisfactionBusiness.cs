using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business
{
    public interface ICustomerSatisfactionBusiness
    {
        List<ClientFeedbackDetailsView> ClientFeedbackDetailsModel(int contractID);
        public bool CreateCustomerFeedback(DateTime? FeedbackDate, int contractID, string clientResponse, int? clientRating, string receivedComplaint, int? Status);
        public List<CustomerSastifaction> CustomerSastifaction();
        public object CustomerContacts(int contractID);
    }
}
