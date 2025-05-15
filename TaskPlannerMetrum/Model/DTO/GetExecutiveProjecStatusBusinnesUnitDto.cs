using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.DTO
{
    public class GetExecutiveProjecStatusBusinnesUnitDto
    {
        [Key]
        public string Periodo { get; set; }
        public int Status { get; set; }

        public int BusinessUnit { get; set; }
        public int TechLeaders { get; set; }
        public int Inspectors { get; set; }
        public int QuantidadeEmAndamento { get; set; }

        public string? ProjetosEmAndamento { get; set; }
        public string? UnidadesNegocioRetornadas { get; set; }
        public string? TechLeadersRetornados { get; set; }
        public string? InspectorsRetornados { get; set; }
    }
}
