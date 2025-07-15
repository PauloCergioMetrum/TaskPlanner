using System.ComponentModel.DataAnnotations;
#nullable enable


namespace TaskPlannerMetrum.Model.DTO
{
   
    
        public class GetAvailableHoursByFunctionDTO
        {
        [Key]  
            public int? DepartmentID { get; set; }              
            public string? DepartmentName { get; set; }         
            public string? Period { get; set; }                
            public string? FunctionName { get; set; }           
            public double? TotalAvailableHours { get; set; }      
        }
    

}
