using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Business
{
    public interface IReportsPlannedExecutedViewewBussines
    {
        public IEnumerable<vReports_PlannedExecuted> ReportsPlannedExecuted(DateTime startDate, DateTime endDate);
    }
}
