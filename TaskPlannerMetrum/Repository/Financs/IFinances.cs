using System.Collections.Generic;
using TaskPlannerMetrum.Model;
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
        public dynamic CreateAllService(Service service);

        public bool DeleteAllService( int ID);
        public dynamic UpdateAllService(Service service);
        public dynamic DuplicateFinance(Model.DTO.DuplicateFinanceDTO Finance);

    }
}
