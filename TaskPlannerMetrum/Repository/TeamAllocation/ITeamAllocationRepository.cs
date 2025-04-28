using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.TeamAllocation
{
    public interface ITeamAllocationRepository
    {


        List<ExecutorHoursTableDTO> GetExecutorHoursTable(DateTime startDate, DateTime endDate);





    }
}