using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.TeamAllocation;
namespace TaskPlannerMetrum.Business
{
    public interface ITeamAllocationBusiness
    {
        List<ExecutorHoursTableDTO> GetExecutorHoursTable(DateTime startDate, DateTime endDate);


    }
}
