using System;
using System.ComponentModel.DataAnnotations;
namespace TaskPlannerMetrum.Model.DTO
{
    public class GetProjectStatusPeriodDTO
    {
        [Key]
        public string BusinessUnit { get; set; }
        public int QuantidadeProjetos { get; set; }


    }
}
