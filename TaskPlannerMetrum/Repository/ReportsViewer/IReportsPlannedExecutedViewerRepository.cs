using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ReportsViewer
{
    public interface IReportsPlannedExecutedViewerRepository
    {
        public List<vReports_PlannedExecuted> ReportsPlannedExecuted(DateTime startDate, DateTime endDate );
        public List<HoursCostModel> GetHourCost(DateTime startDate, DateTime endDate);
        public double GetHourExpectedHour(DateTime startDate, DateTime endDate);


    }
}
