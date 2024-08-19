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
    }
}
