


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

        public double GetHourCost(DateTime startDate, DateTime endDate)
        {
            var listID = _context.vReports_PlannedExecuted.Where(e => e.ScheduledDate.Date >= startDate.Date && e.ScheduledDate.Date <= endDate.Date).Select(i=> i.ExecutorID).ToList();

            double values = 0;

            foreach (var item in listID) 
            {
                values += _context.UserHourCosts.Where(u => u.UserID == item).Select(h => h.HourCost).FirstOrDefault();
            }
            return values;
        }

        public List<vReports_PlannedExecuted> ReportsPlannedExecuted(DateTime startDate, DateTime endDate)
        {
            try
            {
               
                var list = _context.vReports_PlannedExecuted.Where(e => e.ScheduledDate.Date >= startDate.Date && e.ScheduledDate.Date <= endDate.Date).OrderByDescending( e => e.ScheduledDate).ToList();

                //list.Sort((d1, d2) => d1.ScheduledDate.CompareTo(d2.ScheduledDate));



                return list;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}



