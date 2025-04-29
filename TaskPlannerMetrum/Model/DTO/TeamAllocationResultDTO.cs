using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class TeamAllocationResultDTO
    {
        public List<ExecutorHoursTableDTO> ExecutorHoursTable { get; set; }
        public TaskExecutionMetricsDTO TaskExecutionMetrics { get; set; }
        public List<FunctionEmployeeCountTableDTO> FunctionEmployeeCountTable { get; set; }
        public List<AvailableHoursByFunctionTableDTO> AvailableHoursByFunctionTable { get; set; }
        public List<GetPlannedAndExecutedByFunctionDTO> GetPlannedAndExecutedByFunction { get; set; }
    }
    
}
