using System;
using System.ComponentModel.DataAnnotations;
#nullable enable

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vContractExecutionHHCost
    {
  
        [Key]
    
        public int?ContractID { get; set; }
        public string? DisplacementServiceName { get; set; }
       
        public int ExecutorTeamID { get; set; }
        public double? Hours { get; set; }
        public double? TotalPrevisto { get; set; }
        public double? TotalExecutado { get; set; }
        public double? TotalExecutedManHour { get; set; }
        public double? HourCost { get; set; }
        public double? SomaTotalExecutedManHour { get; set; }
        public double? CustoExecutadoTotal { get; set; }

    }
}