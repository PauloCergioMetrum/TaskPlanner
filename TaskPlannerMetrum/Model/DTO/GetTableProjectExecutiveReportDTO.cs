using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.DTO
{
    public class GetTableProjectExecutiveReportDto
    {
        [Key]
        public string InternalCode { get; set; }
        public int ContractID { get; set; }
        public string TechLeaderName { get; set; }
        public string InspectorName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DateFineshed { get; set; }
        public string BusinessUnit { get; set; }
        public int? Status { get; set; }
        public string ClientName { get; set; }
    }
}
