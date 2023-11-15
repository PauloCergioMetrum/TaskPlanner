using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ReportsViewer
{
    public interface IReportsPlannedExecutedViewerRepository
    {
        public IEnumerable<vReports_PlannedExecuted> ReportsPlannedExecuted(DateTime startDate, DateTime endDate);
    }
}
