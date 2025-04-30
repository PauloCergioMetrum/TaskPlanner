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

        public List<ExecutorHoursTableDTO> GetExecutorHoursTable(DateTime startDate, DateTime endDate, string functionName = null, string departmentName = null)
        {
            return _teamAllocationRepository.GetExecutorHoursTable(startDate, endDate, functionName, departmentName);
        }


        public TeamAllocationResultDTO GetTeamAllocationReport(
    DateTime startDate,
    DateTime endDate,
    string functionName = null,
    string departmentName = null) 
        {
            return new TeamAllocationResultDTO
            {
                ExecutorHoursTable = _teamAllocationRepository.GetExecutorHoursTable(startDate, endDate, functionName, departmentName),
                TaskExecutionMetrics = _teamAllocationRepository.GetTaskExecutionMetrics(startDate, endDate,functionName ),
                FunctionEmployeeCountTable = _teamAllocationRepository.GetFunctionEmployeeCount(startDate, endDate, functionName),
                AvailableHoursByFunctionTable = _teamAllocationRepository.GetAvailableHoursByFunction(startDate, endDate, functionName),
                GetPlannedAndExecutedByFunction = _teamAllocationRepository.GetPlannedAndExecutedByFunction(startDate, endDate, functionName),
            };
        }


    }
}
