using System;

namespace TaskPlannerMetrum.Model
{
    public class MilestoneHhGraphic
    {

        public string? DisplacementServiceName { get; set; }
        public double? TotalMilesStone { get; set; }
        public string FunctionName { get; set; }
        public int ContractID { get; set; }
        public string MilestonesValueID { get; set; }
        public double? SomaTotalMilesStone { get; set; }
        public double? TotalExecutedManHour { get; set; }
        public double? CustoPlanejado { get; set; }
        public double? CustoExecutado { get; set; }
        public double? CustoTotalPlanejado { get; set; }
        public double? SomaTotalExecutedManHour { get; set; }
    }
}
