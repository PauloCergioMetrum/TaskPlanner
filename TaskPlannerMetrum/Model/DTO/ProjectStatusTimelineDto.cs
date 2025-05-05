using System;
using System.ComponentModel.DataAnnotations;
namespace TaskPlannerMetrum.Model.DTO
{
    public class ProjectStatusTimelineDto
    {
        [Key]
        public string BusinessUnit { get; set; }
        public string InternalCode { get; set; }
        public int QuantidadeProjetos { get; set; }
    }
}
