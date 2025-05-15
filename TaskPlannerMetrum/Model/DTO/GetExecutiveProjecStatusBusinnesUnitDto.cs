using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.DTO
{
    public class GetExecutiveProjecStatusBusinnesUnitDto
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Periodo { get; set; }
        public int BusinessUnit { get; set; }
        public int QuantidadeEmAndamento { get; set; }
        public int TechLeaders { get; set; }
        public int Inspectors { get; set; }
        public int Status { get; set; }
    }
}
