using System;
using System.ComponentModel.DataAnnotations;
namespace TaskPlannerMetrum.Model.DTO
{
    public class OperationalReportFilterDto
    {
         [Key]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? FiltroStatusID { get; set; }
        public string InternalCode { get; set; }
        public string BusinessUnit { get; set; }
        public string ProjectManager { get; set; }
        public string TechLeader { get; set; }
        public string ProjectStatus { get; set; }
    }
}