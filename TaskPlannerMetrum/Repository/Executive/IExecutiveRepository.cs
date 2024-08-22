using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.Executive
{
    public interface IExecutiveRepository
    {
        List<ExecutiveDto> GetPvExecutiveTable(string inspectorIDs = null, DateTime? startDate = null, DateTime? endDate = null);


        List<GetPvSummary> GetPvSummaries(DateTime? StartYearMonth, DateTime? EndYearMonth);

        List<BusinessUnitPercentage> GetBusinessUnitPercentages();

        List<ExecutivePVgraphic> GetExecutivePVGraphic();



    }

}
