using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class StatusReportGraphHH
    {
        [Key]
        public int ContractID { get; set; }

        public double? CustoTotalPrevistoServico { get; set; }

        public decimal? CustoTotalPlanejadoServico { get; set; }

        public decimal? CustoTotalExecutadoServico { get; set; }

        public double? CustoTotalPrevistoDeslocamento { get; set; }

        public decimal? CustoTotalPlanejadoDeslocamento { get; set; }

        public decimal? CustoTotalExecutadoDeslocamento { get; set; }
    }
}
