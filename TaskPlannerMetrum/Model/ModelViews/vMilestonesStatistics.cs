using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vMilestonesStatistics
    {

        [Key]
        public string DisplacementServiceName { get; set; }
        public double? TotalMilesStone { get; set; }
        public string FunctionName { get; set; }
        public int ContractID { get; set; }
        public string MilestonesValueID { get; set; }
        public double? SomaTotalMilesStone { get; set; }
        public decimal? TotalExecutedManHour { get; set; }
        public decimal? CustoPlanejado { get; set; }
        public decimal? CustoExecutado { get; set; }
        public decimal? CustoTotalPlanejado { get; set; }
        public decimal? SomaTotalExecutedManHour { get; set; }
    }
}
