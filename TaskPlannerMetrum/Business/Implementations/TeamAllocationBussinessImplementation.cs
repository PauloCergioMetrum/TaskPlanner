using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.TeamAllocation;

namespace TaskPlannerMetrum.Business
{
    public class TeamAllocationBusinessImplementation : ITeamAllocationBusiness
    {
        private readonly ITeamAllocationRepository _teamAllocationRepository;

        public TeamAllocationBusinessImplementation(ITeamAllocationRepository teamAllocationRepository)
        {
            _teamAllocationRepository = teamAllocationRepository;
        }

        public List<ExecutorHoursTableDTO> GetExecutorHoursTable(DateTime startDate, DateTime endDate)
        {
            return _teamAllocationRepository.GetExecutorHoursTable(startDate, endDate);
        }

        public TeamAllocationResultDTO GetTeamAllocationReport(DateTime startDate, DateTime endDate, string functionName = null)
        {
            return new TeamAllocationResultDTO
            {
                ExecutorHoursTable = _teamAllocationRepository.GetExecutorHoursTable(startDate, endDate),
                TaskExecutionMetrics = _teamAllocationRepository.GetTaskExecutionMetrics(startDate, endDate),
                FunctionEmployeeCountTable = _teamAllocationRepository.GetFunctionEmployeeCount(startDate, endDate, functionName),
                AvailableHoursByFunctionTable = _teamAllocationRepository.GetAvailableHoursByFunction(startDate, endDate, functionName)
            };
        }

    }
}
