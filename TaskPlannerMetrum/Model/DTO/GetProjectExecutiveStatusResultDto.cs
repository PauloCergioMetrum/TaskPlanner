using System.ComponentModel.DataAnnotations;
#nullable enable

namespace TaskPlannerMetrum.Model.DTO
{
    public class GetProjectExecutiveStatusResultDto
    {
        [Key]
        public int? ContractID { get; set; }
        public string? BusinessUnit { get; set; }
        public string? InternalCode { get; set; }
        public string? InspectorName { get; set; }
        public string? TechLeaderName { get; set; }
        public int ?Status { get; set; }


}
}
