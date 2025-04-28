using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.TeamAllocation
{
    public interface ITeamAllocationRepository
    {
        List<ExecutorHoursTableDTO> GetExecutorHoursTable(DateTime startDate, DateTime endDate);
        TaskExecutionMetricsDTO GetTaskExecutionMetrics(DateTime startDate, DateTime endDate);
        List<FunctionEmployeeCountTableDTO> GetFunctionEmployeeCount(DateTime startDate, DateTime endDate, string functionName = null);

    }
}
