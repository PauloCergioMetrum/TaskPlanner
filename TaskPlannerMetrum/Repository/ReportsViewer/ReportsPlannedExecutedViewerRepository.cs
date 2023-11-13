


using System;
using System.Linq;
using TaskPlannerMetrum.Model.Context;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskPlannerMetrum.Repository.ReportsViewer
{
    public class ReportsPlannedExecutedViewerRepository : IReportsPlannedExecutedViewerRepository
    {
        private readonly MSSQLContext _context;

        public ReportsPlannedExecutedViewerRepository(MSSQLContext context)
        {
            _context = context;
        }

        public dynamic ReportsPlannedExecuted()
        {
            try
            {
             
                return _context.vReports_PlannedExecuted.OrderBy(d => d.inspectorID).ToList();
            }
            catch (Exception ex)
            {

                return false; 
            }
        }
    }

}



