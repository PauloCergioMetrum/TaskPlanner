using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ContractStatusSummaryDto
    {
        [Key]
        public string Periodo { get; set; }              // Ex: "2025-04"
        public string BusinessUnit { get; set; }
        public string InternalCode { get; set; }
        public int QuantidadeAbertos { get; set; }
        public int QuantidadeFechados { get; set; }
    }
}