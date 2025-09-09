using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class CardsHHHours
    {
        [Key]
        public string SourceType { get; set; }       
        public string DepartmentName { get; set; }      
        public string DisplacementServiceName { get; set; }
        public double? Hours { get; set; }             
        public double? TotalMilesStone { get; set; }   
        public string FunctionName { get; set; }        
        public int ContractID { get; set; }            
        public double? ValueHour { get; set; }         
        public double? TotalExecutedManHour { get; set; }
        public double? TotalPlannedManHour { get; set; } 
        public decimal? CustoPlanejado { get; set; }    
        public decimal? CustoExecutado { get; set; }    
        public double? DiferencaHoras { get; set; }    
        public double? DiferencaDinheiro { get; set; }  
    }
}

