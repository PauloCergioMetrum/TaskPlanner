using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.Financs
{
    public interface IFinances
    {
        public List<Model.ModelViews.vFinanceContract> GetAllFinances();

        dynamic GetFinancesById(int contractId);

        public bool Create(Model.DTO.financeDTO newfinance);

        public bool UpdateFinances(Model.Finances finances);

        public bool DeleteFinance(int id);

        public dynamic GetContractInfo(int id);

        public dynamic getAllServices(string type);

        public dynamic DuplicateFinance(Model.DTO.DuplicateFinanceDTO Finance);

    }
}
