using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.CustomerSatisfaction;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class CustomerSatisfactionImplementation : ICustomerSatisfactionBusiness
    {
        private readonly ICustomerSatisfactionRepository _repository;

        public CustomerSatisfactionImplementation(ICustomerSatisfactionRepository customerSatisfactionRepository)
        {
            _repository = customerSatisfactionRepository;
        }

        public List<CustomerSatisfactionDTO> ClientFeedbackDetailsModel(int contractID)
        {
            return _repository.ClientFeedbackDetailsModel(contractID);
        }

        public bool CreateCustomerFeedback(DateTime FeedbackDate, int contractID, string clientResponse, int? clientRating, string receivedComplaint)
        {
            return _repository.CreateCustomerFeedback(FeedbackDate,contractID, clientResponse, clientRating, receivedComplaint);  
        }
    }
}
