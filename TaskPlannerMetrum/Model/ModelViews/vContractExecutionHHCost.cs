using System;
using System.ComponentModel.DataAnnotations;
#nullable enable

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vContractExecutionHHCost
    {
  
        [Key]
        public string MilestonesValueID { get; set; }
        public int ContractID { get; set; }
        public string DisplacementServiceName { get; set; }
        public decimal Hours { get; set; }
        public decimal TotalPrevisto { get; set; }
        public decimal TotalExecutado { get; set; }
        public decimal TotalExecutedManHour { get; set; }
        public int ExecutorTeamID { get; set; }
        public double? HourCost { get; set; }
        public decimal SomaTotalExecutedManHour { get; set; }
        public double CustoExecutadoTotal { get; set; }
    }
}