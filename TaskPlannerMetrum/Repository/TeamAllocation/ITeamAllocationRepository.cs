using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.TeamAllocation
{
    public interface ITeamAllocationRepository
    {
        TaskExecutionMetricsDTO GetTaskExecutionMetrics(
            DateTime startDate,
            DateTime endDate,
            string functionName = null,
            string departmentName = null);

        List<GetPlannedAndExecutedByFunctionDTO> GetPlannedAndExecutedByFunction(
            DateTime startDate,
            DateTime endDate,
            string functionName = null,
            string departmentName = null);

        List<FunctionEmployeeCountTableDTO> GetFunctionEmployeeCount(
            DateTime startDate,
            DateTime endDate,
            string functionName = null,
            string departmentName = null);

        List<GetAvailableHoursByFunctionDTO> GetAvailableHoursByFunction(
            DateTime startDate,
            DateTime endDate,
            string functionName = null,
            string departmentName = null);

        List<ExecutorHoursTableDTO> GetExecutorHoursTable(
            DateTime startDate,
            DateTime endDate,
            string functionName = null,
            string departmentName = null);
    }
}
