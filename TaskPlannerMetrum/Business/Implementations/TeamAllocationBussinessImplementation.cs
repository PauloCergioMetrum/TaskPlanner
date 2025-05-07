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




        public TeamAllocationResultDTO GetTeamAllocationReport(DateTime startDate, DateTime endDate, string functionName = null, string departmentName = null)
        {
            return new TeamAllocationResultDTO
            {
                TaskExecutionMetrics = _teamAllocationRepository.GetTaskExecutionMetrics(startDate, endDate, functionName, departmentName),
                ExecutorHoursTable = _teamAllocationRepository.GetExecutorHoursTable(startDate, endDate, functionName, departmentName),
                GetPlannedAndExecutedByFunction = _teamAllocationRepository.GetPlannedAndExecutedByFunction(startDate, endDate, functionName, departmentName),
                AvailableHoursByFunctionTable = _teamAllocationRepository.GetAvailableHoursByFunction(startDate, endDate, functionName, departmentName),
                FunctionEmployeeCountTable = _teamAllocationRepository.GetFunctionEmployeeCount(startDate, endDate, functionName, departmentName)
            };
        }





    }
}
