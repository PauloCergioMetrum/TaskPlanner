using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.TeamAllocation;

namespace TaskPlannerMetrum.Business
{
    public class TeamAllocationBussinessImplementation : ITeamAllocationBusiness
    {
        private readonly ITeamAllocationRepository _teamAllocationRepository;

        public TeamAllocationBussinessImplementation(ITeamAllocationRepository teamAllocationRepository)
        {
            _teamAllocationRepository = teamAllocationRepository;
        }

        public List<ExecutorHoursTableDTO> GetExecutorHoursTable(DateTime startDate, DateTime endDate)
        {
            return _teamAllocationRepository.GetExecutorHoursTable(startDate, endDate);
        }
    }
}
