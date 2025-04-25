using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace TaskPlannerMetrum.Repository.GetContractsByMonthRepository
{
    public class GetContractsByMonthRepository : IGetContractsByMonthRepository
    {
        private MSSQLContext _context;

        public GetContractsByMonthRepository(MSSQLContext context)
        {
            _context = context;
        }

        public async Task<List<GetContractsByMonthDto>> GetAsync(DateTime startDate, DateTime endDate)
        {
            var startParam = new SqlParameter("@StartDate", startDate);
            var endParam = new SqlParameter("@EndDate", endDate);

            return await _context.Set<GetContractsByMonthDto>()
                .FromSqlRaw("EXEC GetContractsByMonth @StartDate, @EndDate", startParam, endParam)
                .ToListAsync();
        }
    }

}
