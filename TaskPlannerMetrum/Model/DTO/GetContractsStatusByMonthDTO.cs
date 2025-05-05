using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.DTO
{
    public class GetContractsStatusByMonthDTO
    {

        [Key]
        public string InternalCode { get; set; }
        public string Periodo { get; set; }               // yyyy-MM
        public string BusinessUnit { get; set; }          // Nome da unidade de negócio
        public int QuantidadeAbertos { get; set; }        // Total de contratos abertos no mês
        public int QuantidadeFechados { get; set; }       // Total de contratos fechados no mês
    }
}
