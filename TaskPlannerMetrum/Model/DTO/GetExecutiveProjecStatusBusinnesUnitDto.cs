using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.DTO
{
    public class GetExecutiveProjecStatusBusinnesUnitDto
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Periodo { get; set; }
        public string BusinessUnit { get; set; }
        public int QuantidadeEmAndamento { get; set; }
        public string TechLeaders { get; set; }
        public string Inspectors { get; set; }
        public string Status { get; set; }
    }
}
