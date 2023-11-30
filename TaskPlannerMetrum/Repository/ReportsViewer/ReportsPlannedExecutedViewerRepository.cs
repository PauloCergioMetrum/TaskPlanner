


using Microsoft.EntityFrameworkCore;
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
            var listExecutorID = _context.vReports_PlannedExecuted.Where(e => e.ScheduledDate.Date >= startDate.Date && e.ScheduledDate.Date <= endDate.Date).Select(i=> i.ExecutorID).Distinct().ToList();


            var listCost = _context.UserHourCosts.Where(e => e.StartDate <= startDate && e.EndDate >= endDate && listExecutorID.Contains(e.UserID)).Select(h => h.HourCost).ToList();


            var resulHourCost = Math.Round(listCost.Sum(),2) ;

            return resulHourCost;

           
        }

        public List<vReports_PlannedExecuted> ReportsPlannedExecuted(DateTime startDate, DateTime endDate)
        {
            try
            {
               
                var list = _context.vReports_PlannedExecuted.Where(e => e.ScheduledDate.Date >= startDate.Date && e.ScheduledDate.Date <= endDate.Date).OrderByDescending( e => e.ScheduledDate).ToList();

                return list;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}



