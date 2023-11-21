using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Business
{
    public interface IReportsPlannedExecutedViewewBussines
    {
        public ReportPlannedExecuted GetPlannedExecuted(DateTime startDate, DateTime endDate);
    }
}
