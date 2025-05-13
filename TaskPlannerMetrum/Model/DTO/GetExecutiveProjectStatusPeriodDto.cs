using System;
using System.ComponentModel.DataAnnotations;
namespace TaskPlannerMetrum.Model.DTO
{
    public class GetExecutiveProjectStatusPeriodDto
    {
        [System.ComponentModel.DataAnnotations.Key]

        public string InternalCodes { get; set; }
        public string BusinessUnit { get; set; }
        public int QuantidadeProjetos { get; set; }
        public string Status { get; set; }


    }
}
