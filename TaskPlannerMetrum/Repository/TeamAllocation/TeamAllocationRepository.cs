using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using System.Linq;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.TeamAllocation
{
    public class TeamAllocationRepository : ITeamAllocationRepository
    {
        private readonly MSSQLContext _context;

        public TeamAllocationRepository(MSSQLContext context)
        {
            _context = context;
        }

        public List<ExecutorHoursTableDTO> GetExecutorHoursTable(DateTime startDate, DateTime endDate)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);

            var result = _context.Set<ExecutorHoursTableDTO>()
                .FromSqlRaw("EXEC GetExecutorHoursTable @StartDate, @EndDate", startParam, endParam)
                .ToList();

            return result;
        }

        // novo 
    }
}
