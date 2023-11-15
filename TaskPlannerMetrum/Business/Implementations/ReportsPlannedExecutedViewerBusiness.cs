using System;
using System.Collections.Generic;
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
    }


}
