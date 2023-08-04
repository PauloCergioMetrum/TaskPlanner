using System;
using System.Linq;
using TaskPlannerMetrum.Model.Context;

namespace TaskPlannerMetrum.Repository.ContractViewer
{
    public class ContractViewerRepository:IContractViewerRepository
    {

        private readonly MSSQLContext _context;

        public ContractViewerRepository(MSSQLContext context)
        {
            _context=context;
        }

        public dynamic GetAllContractsView(string Date)
        {
            return _context.vContractViewer.Where(d => d.MES_PREVISTO_FATURAMENTO == Date).ToList();
        }
    }
}
