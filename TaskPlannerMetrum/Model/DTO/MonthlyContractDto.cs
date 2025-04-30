using System;
using System.ComponentModel.DataAnnotations;
namespace TaskPlannerMetrum.Model.DTO
{
    public class MonthlyContractDto
    {
        [Key]
        public string PeriodMonth { get; set; }
        public string BusinessUnit { get; set; }
        public string InternalCode { get; set; }
        public int ProjectCountPerBusinessUnit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}

