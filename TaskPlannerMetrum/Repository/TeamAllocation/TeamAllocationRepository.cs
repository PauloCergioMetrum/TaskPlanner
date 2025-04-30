using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.TeamAllocation
{
    public class TeamAllocationRepository : ITeamAllocationRepository
    {
        private readonly MSSQLContext _context;

        public TeamAllocationRepository(MSSQLContext context)
        {
            _context = context;
        }

        public List<ExecutorHoursTableDTO> GetExecutorHoursTable(DateTime startDate, DateTime endDate, string functionName = null, string departmentName = null)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);
            var functionParam = new SqlParameter("@FunctionName", (object?)functionName ?? DBNull.Value);
            var departmentParam = new SqlParameter("@DepartmentName", (object?)departmentName ?? DBNull.Value);

            return _context.Set<ExecutorHoursTableDTO>()
                .FromSqlRaw("EXEC GetExecutorHoursTable @StartDate, @EndDate, @FunctionName, @DepartmentName",
                            startParam, endParam, functionParam, departmentParam)
                .ToList();
        }

        public TaskExecutionMetricsDTO GetTaskExecutionMetrics(DateTime startDate, DateTime endDate, string functionName = null)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);
            var functionParam = new SqlParameter("@FunctionName", functionName ?? (object)DBNull.Value);

            return _context.Set<TaskExecutionMetricsDTO>()
                .FromSqlRaw("EXEC GetTaskExecutionMetrics @StartDate, @EndDate, @FunctionName",
                    startParam, endParam, functionParam)
                .AsEnumerable()
                .FirstOrDefault() ?? new TaskExecutionMetricsDTO();
        }


        public List<FunctionEmployeeCountTableDTO> GetFunctionEmployeeCount(DateTime startDate, DateTime endDate, string functionName = null)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);
            var functionNameParam = new SqlParameter("@FunctionName", (object)functionName ?? DBNull.Value);

            return _context.Set<FunctionEmployeeCountTableDTO>()
                .FromSqlRaw("EXEC GetFunctionEmployeeCount @StartDate, @EndDate, @FunctionName", startParam, endParam, functionNameParam)
                .ToList();
        }

        public List<AvailableHoursByFunctionTableDTO> GetAvailableHoursByFunction(DateTime startDate, DateTime endDate, string functionName = null)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);
            var functionNameParam = new SqlParameter("@FunctionName", (object)functionName ?? DBNull.Value);

            return _context.Set<AvailableHoursByFunctionTableDTO>()
                .FromSqlRaw("EXEC GetAvailableHoursByFunction @StartDate, @EndDate, @FunctionName", startParam, endParam, functionNameParam)
                .ToList();
        }


        public List<GetPlannedAndExecutedByFunctionDTO> GetPlannedAndExecutedByFunction(DateTime startDate, DateTime endDate, string functionName = null)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);
            var functionNameParam = new SqlParameter("@FunctionName", (object)functionName ?? DBNull.Value);

            return _context.Set<GetPlannedAndExecutedByFunctionDTO>()
                .FromSqlRaw("EXEC GetPlannedAndExecutedByFunction @StartDate, @EndDate, @FunctionName", startParam, endParam, functionNameParam)
                .ToList();
        }

    }
}
