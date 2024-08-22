using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using TaskPlannerMetrum.Business.Implementations;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.Executive;
using TaskPlannerMetrum.Repository.Financs;
using TaskPlannerMetrum.Repository.TeamAllocation;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ExecutiveImplentations : IExecutiveBussines
    {

        private readonly IExecutiveRepository _executiveRepository;

        public ExecutiveImplentations(IExecutiveRepository executiveRepository)
        {
            _executiveRepository = executiveRepository;
        }


        public ExecutiveDto GetPvExecutiveTable(string inspectorIDs = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var allocations = _executiveRepository.GetPvExecutiveTable(inspectorIDs, startDate, endDate);
            return allocations.FirstOrDefault();
        }

        public List<GetPvSummary> GetPvSummaries(DateTime? StartYearMonth, DateTime? EndYearMonth)
        {
            var summaries = _executiveRepository.GetPvSummaries(StartYearMonth, EndYearMonth);
            return summaries.ToList();
        }

        public List<BusinessUnitPercentage> GetBusinessUnitPercentages()
        {
            var getbusinessPorcentage = _executiveRepository.GetBusinessUnitPercentages();
            return getbusinessPorcentage.ToList();
        }

        public List<ExecutivePVgraphic> GetExecutivePVGraphic()
        {
            var executivePVGraphic = _executiveRepository.GetExecutivePVGraphic();
            return executivePVGraphic.ToList();

        }

        public ExecutiveDtoAll CreateExecutiveDtoAll(string inspectorIDs = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var executiveList = _executiveRepository.GetPvExecutiveTable(inspectorIDs, startDate, endDate);
            var summaryList = _executiveRepository.GetPvSummaries(startDate, endDate);
            var getbusinessPorcentage = _executiveRepository.GetBusinessUnitPercentages();
            var executivePVGraphic = _executiveRepository.GetExecutivePVGraphic();

            return new ExecutiveDtoAll
            {
                ExecutiveDtoList = executiveList,
                GetPvSummaryList = summaryList,
                businessUnitPercentages = getbusinessPorcentage,
                executivePVgraphics = executivePVGraphic

            };
        }


    }
}





