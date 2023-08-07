using System.Collections.Generic;
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

        public dynamic DuplicateFinance(DuplicateFinanceDTO Finance);
    }
}
