using System;

namespace TaskPlannerMetrum.Model.DTO
{
#nullable enable
    public class OperationalRelationshipTable
    {
        public int? ContractID { get; set; }

        public string? InternalCode { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? BusinessUnits { get; set; }

        public string? ProjectInspector { get; set; }

        public string? ClientName { get; set; }

        public string? TechLead { get; set; }
    }
}
