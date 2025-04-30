using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.TeamAllocation
{
    public interface ITeamAllocationRepository
    {
    
        List<ExecutorHoursTableDTO> GetExecutorHoursTable(DateTime startDate, DateTime endDate, string functionName = null, string departmentName = null);
        TaskExecutionMetricsDTO GetTaskExecutionMetrics(DateTime startDate, DateTime endDate);
        List<FunctionEmployeeCountTableDTO> GetFunctionEmployeeCount(DateTime startDate, DateTime endDate, string functionName = null);
        List<AvailableHoursByFunctionTableDTO> GetAvailableHoursByFunction(DateTime startDate, DateTime endDate, string functionName = null);
        List<GetPlannedAndExecutedByFunctionDTO> GetPlannedAndExecutedByFunction(DateTime startDate, DateTime endDate, string functionName = null);


    }
}
