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

        public TeamAllocationResultDTO GetTeamAllocationComplete(DateTime startDate, DateTime endDate)
        {
            var executorHours = _teamAllocationRepository.GetExecutorHoursTable(startDate, endDate);
            var metrics = _teamAllocationRepository.GetTaskExecutionMetrics(startDate, endDate);

            return new TeamAllocationResultDTO
            {
                ExecutorHoursTable = executorHours,
                TaskExecutionMetrics = metrics
            };
        }

        public TeamAllocationResultDTO GetTeamAllocationComplete(DateTime startDate, DateTime endDate, string functionName = null)
        {
            var executorHours = _teamAllocationRepository.GetExecutorHoursTable(startDate, endDate);
            var metrics = _teamAllocationRepository.GetTaskExecutionMetrics(startDate, endDate);
            var functionEmployeeCount = _teamAllocationRepository.GetFunctionEmployeeCount(startDate, endDate, functionName);

            return new TeamAllocationResultDTO
            {
                ExecutorHoursTable = executorHours,
                TaskExecutionMetrics = metrics,
                FunctionEmployeeCountTable = functionEmployeeCount
            };
        }


    }
}
