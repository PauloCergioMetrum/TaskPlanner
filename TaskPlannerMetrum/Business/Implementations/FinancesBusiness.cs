using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Repository.Contracts;
using TaskPlannerMetrum.Repository.Financs;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class FinancesBusiness : IFinancesBusiness
    {

        private readonly IFinances _financestRepository;
     


        public FinancesBusiness(IFinances financesrepository)
        {
            _financestRepository = financesrepository;

        }

        public bool Create(Model.DTO.financeDTO newfinance)
        {
            return _financestRepository.Create(newfinance);
        }

        public bool DeleteFinance(int id)
        {
            return _financestRepository.DeleteFinance(id);  
        }

        public dynamic GetAllFinances()
        {
           return  _financestRepository.GetAllFinances();  
        }

       public  dynamic GetFinancesById(int contractId)
        {
            return _financestRepository.GetFinancesById(contractId);
        }

        public dynamic GetContractInfo(int id)
        {
            return _financestRepository.GetContractInfo(id);
        }

        public bool UpdateFinances(Model.Finances newfinance)
        {
            return _financestRepository.UpdateFinances(newfinance);
        }

        public dynamic getAllServices(string type)
        {
            return _financestRepository.getAllServices(type);
        }

        public dynamic DuplicateFinance(DuplicateFinanceDTO Finance)
        {
            return _financestRepository.DuplicateFinance(Finance);
        }
    }
}
