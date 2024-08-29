using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business
{
    public interface IExecutiveBussines
    {
        ExecutiveDtoAll CreateExecutiveDtoAll(string inspectorIDs = null, DateTime? startDate = null, DateTime? endDate = null , string businessUnits = null);
       
        List<GetPvSummary> GetPvSummaries(DateTime? StartYearMonth, DateTime? EndYearMonth);

        List<BusinessUnitPercentage> GetBusinessUnitPercentages();
        List<ExecutivePVgraphic> GetExecutivePVGraphic();
   

        public List<ExecutivePvGraphicsResult> GetExecutivePvGraphicsOpenClose(bool? closed = null, bool? open = null);



    }

}


