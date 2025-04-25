using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.GetContractsByMonthRepository
{
    public interface IGetContractsByMonthRepository
    {
        Task<List<GetContractsByMonthDto>> GetAsync(DateTime startDate, DateTime endDate);

    }
}
