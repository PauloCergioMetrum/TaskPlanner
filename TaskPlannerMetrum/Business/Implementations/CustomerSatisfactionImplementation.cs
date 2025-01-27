using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;
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

        public List<ClientFeedbackDetailsView> ClientFeedbackDetailsModel(int contractID)
        {
            return _repository.ClientFeedbackDetailsModel(contractID);
        }
        public object CustomerContacts(int contractID)
        {
            return _repository.CustomerContacts(contractID);    
        }

        public bool CreateCustomerFeedback(DateTime? FeedbackDate, int contractID, string clientResponse, int? clientRating, string receivedComplaint , int? Status)
        {
            return _repository.CreateCustomerFeedback(FeedbackDate,contractID, clientResponse, clientRating, receivedComplaint ,Status);  
        }

        public List<CustomerSastifaction> CustomerSastifaction()
        {
            return _repository.CustomerSastifaction();  
        }
    }
}
