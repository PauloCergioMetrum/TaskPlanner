using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class GetTableProjectExecutiveReportDTO
    {
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
