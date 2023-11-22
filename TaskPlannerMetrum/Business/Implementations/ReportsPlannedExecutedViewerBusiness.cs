using System;
using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Repository.ReportsViewer;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ReportsPlannedExecutedViewerBusiness : IReportsPlannedExecutedViewewBussines
    {
        private readonly IReportsPlannedExecutedViewerRepository _repository;

        public ReportsPlannedExecutedViewerBusiness(IReportsPlannedExecutedViewerRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<vReports_PlannedExecuted> ReportsPlannedExecuted(DateTime startDate, DateTime endDate)
        {
            return _repository.ReportsPlannedExecuted(startDate, endDate);
        }

        public ReportPlannedExecuted GetPlannedExecuted(DateTime startDate, DateTime endDate)
        {
            var listPlannedExecuted = _repository.ReportsPlannedExecuted(startDate, endDate);
            var plannedExecuted = new ReportPlannedExecuted
            {
                listPlannedExecuted = listPlannedExecuted,
                totalPlanned = Math.Round(listPlannedExecuted.Sum(s => s.PlannedManHour),2),
                totalExecuted = Math.Round(listPlannedExecuted.Sum(s => s.ExecutedManHour) ,2 ),
                totalHours=Math.Round(_repository.GetHourCost(startDate, endDate),2),

                //totalHours =_repository.GetHourCost(startDate, endDate),



            };

            return plannedExecuted;
        }
    }


}
