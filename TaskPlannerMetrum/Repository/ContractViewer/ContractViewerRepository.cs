using System;
using System.Globalization;
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


            DateTime parsedDate = DateTime.ParseExact(Date, "MM/yyyy", CultureInfo.InvariantCulture);
            DateTime startDate = new DateTime(parsedDate.Year, parsedDate.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            return _context.vContractViewer
                           .Where(d => d.BillingMonth.CompareTo(startDateString) >= 0 &&
                                       d.BillingMonth.CompareTo(endDateString) <= 0)
                           .OrderBy(d => d.BillingMonth)
                           .ToList();

        }
    }
}
