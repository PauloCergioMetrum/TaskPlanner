using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.CustomerSatisfaction
{
    public interface ICustomerSatisfactionRepository
    {


        public List<ClientFeedbackDetailsView> ClientFeedbackDetailsModel(int contractID);
        public bool CreateCustomerFeedback(DateTime? FeedbackDate, int contractID, string clientResponse, int? clientRating, string receivedComplaint , int? Status);
        public List<CustomerSastifaction> CustomerSastifaction();

    }
}

