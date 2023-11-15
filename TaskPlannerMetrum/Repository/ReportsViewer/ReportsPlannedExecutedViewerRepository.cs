


using System;
using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.ModelViews;
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

        public IEnumerable<vReports_PlannedExecuted> ReportsPlannedExecuted(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _context.vReports_PlannedExecuted
                    .Where(e => e.ScheduledDate.Date >= startDate.Date && e.ScheduledDate.Date <= endDate.Date)
                    .OrderBy(d => d.inspectorID)
                    .ToList();
            }
            catch (Exception ex)
            {
                return null; 
            }
        }
    }

}



