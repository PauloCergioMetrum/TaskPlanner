using Microsoft.EntityFrameworkCore;
using System;

namespace TaskPlannerMetrum.Model.DTO
{

    [Keyless]
    public class GetContractsByMonthDto
    {
        public DateTime PeriodMonth { get; set; }
        public int ContractID { get; set; }
        public string BusinessUnits { get; set; } = string.Empty;
        public int ProjectCountPerBusinessUnit { get; set; }
    }
}
