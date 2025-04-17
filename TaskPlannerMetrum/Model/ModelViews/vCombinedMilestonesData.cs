using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskPlannerMetrum.Model.ModelViews
{
 
    public class CombinedMilestonesData
    {
        [Key]   
        public string? MilestonesValueID { get; set; }
        public int? ContractID { get; set; }
        public string? FunctionName { get; set; }
        public string? DepartmentName { get; set; }
        public string? DisplacementServiceName { get; set; }
       
        public string? DataSource { get; set; } = string.Empty;


        public double? Hours { get; set; }
        public double? TotalMilestones { get; set; }
        public double? TotalPlannedManHour { get; set; }
        public double? TotalExecutedManHour { get; set; }
        public decimal? CustoPlanejado { get; set; }
        public decimal? CustoExecutado { get; set; }
        public decimal? TotalCustos { get; set; }
         public double? Soma { get; set; }
        public decimal? SomaServico { get; set; }
    }
}
