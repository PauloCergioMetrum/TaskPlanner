using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ReportsViewer
{
    public interface IReportsPlannedExecutedViewerRepository
    {
        public List<vReports_PlannedExecuted> ReportsPlannedExecuted(DateTime startDate, DateTime endDate );
        public double  GetHourCost(DateTime startDate, DateTime endDate);

    }
}
