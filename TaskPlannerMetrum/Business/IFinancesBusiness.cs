using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business
{
    public interface IFinancesBusiness
    {
        dynamic GetAllFinances();

        dynamic GetFinancesById(int contractId);

        public bool Create(Model.DTO.financeDTO newfinance);

        public bool UpdateFinances(Model.Finances newfinance);
        public bool DeleteFinance(int id);

        public dynamic GetContractInfo(int id);

        public dynamic getAllServices(string type);
        public bool DeleteAllService(int ID);
        public string UpdateAllService(Service service);
        public dynamic DuplicateFinance(DuplicateFinanceDTO Finance);
        public string CreateAllService(Model.Service service); 
    }
}
