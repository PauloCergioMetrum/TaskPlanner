using System;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.ContractViewer;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ContractViewerBusiness:IContractViewerBusiness
    {

        private readonly IContractViewerRepository _repository;

        public ContractViewerBusiness(IContractViewerRepository repository)
        {
            _repository = repository;
        }

        public dynamic GetAllContractsView(string Date)
        {
         return _repository.GetAllContractsView(Date);  
        }

        
    }
}
