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

        // Relatórios de Alocação de Equipe - Cards
        public TaskExecutionMetricsDTO GetTaskExecutionMetrics(DateTime startDate, DateTime endDate, string functionName = null, string departmentName = null)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);
            var departmentParam = new SqlParameter("@DepartmentName", (object?)departmentName ?? DBNull.Value);
            var functionParam = new SqlParameter("@FunctionName", (object?)functionName ?? DBNull.Value);

            return _context.Set<TaskExecutionMetricsDTO>()
                .FromSqlRaw("EXEC GetTaskExecutionMetrics @StartDate, @EndDate, @DepartmentName, @FunctionName",
                    startParam, endParam, departmentParam, functionParam)
                .AsEnumerable()
                .FirstOrDefault() ?? new TaskExecutionMetricsDTO();
        }


        // Tabela 
        public List<ExecutorHoursTableDTO> GetExecutorHoursTable(
         DateTime startDate,
         DateTime endDate,
         string functionName = null,
         string departmentName = null)
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


        // Relatório de Horas Planejadas e Executadas por Função
        public List<GetPlannedAndExecutedByFunctionDTO> GetPlannedAndExecutedByFunction(DateTime startDate, DateTime endDate, string functionName = null, string departmentName = null)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);
            var functionNameParam = new SqlParameter("@FunctionName", (object?)functionName ?? DBNull.Value);
            var departmentNameParam = new SqlParameter("@DepartmentName", (object?)departmentName ?? DBNull.Value);

            return _context.Set<GetPlannedAndExecutedByFunctionDTO>()
                .FromSqlRaw("EXEC GetPlannedAndExecutedByFunction @StartDate, @EndDate, @FunctionName, @DepartmentName",
                    startParam, endParam, functionNameParam, departmentNameParam)
                .ToList();
        }

        // Relatório de Horas Disponíveis por Função
        public List<GetAvailableHoursByFunctionDTO> GetAvailableHoursByFunction(
       DateTime startDate,
       DateTime endDate,
       string functionName = null,
       string departmentName = null)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);
            var functionNameParam = new SqlParameter("@FunctionName", (object?)functionName ?? DBNull.Value);
            var departmentNameParam = new SqlParameter("@DepartmentName", (object?)departmentName ?? DBNull.Value);

            return _context.Set<GetAvailableHoursByFunctionDTO>()
                .FromSqlRaw("EXEC GetAvailableHoursByFunction @StartDate, @EndDate, @FunctionName, @DepartmentName",
                    startParam, endParam, functionNameParam, departmentNameParam)
                .ToList();
        }


        // Quadro de Funções
        public List<FunctionEmployeeCountTableDTO> GetFunctionEmployeeCount(DateTime startDate, DateTime endDate, string functionName = null, string departmentName = null)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);
            var functionNameParam = new SqlParameter("@FunctionName", (object?)functionName ?? DBNull.Value);
            var departmentNameParam = new SqlParameter("@DepartmentName", (object?)departmentName ?? DBNull.Value);

            return _context.Set<FunctionEmployeeCountTableDTO>()
                .FromSqlRaw("EXEC GetFunctionEmployeeCount @StartDate, @EndDate, @FunctionName, @DepartmentName",
                            startParam, endParam, functionNameParam, departmentNameParam)
                .ToList();
        }
    }
}
