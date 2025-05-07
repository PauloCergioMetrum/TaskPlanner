using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Business
{
    public interface ITeamAllocationBusiness
    {
        TeamAllocationResultDTO GetTeamAllocationReport(
            DateTime startDate,
            DateTime endDate,
         
            string functionName = null,
            string departmentName = null);  
    }


}
