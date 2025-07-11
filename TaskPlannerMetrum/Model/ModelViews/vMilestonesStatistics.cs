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
 

        public double? TotalExecutedManHour { get; set; }
        public double? CustoPlanejado { get; set; }
        public double? CustoExecutado { get; set; }
        public double? CustoTotalPlanejado { get; set; }

    }
}
