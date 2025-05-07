namespace TaskPlannerMetrum.Model.DTO
{
   
    
        public class GetAvailableHoursByFunctionDTO
        {
            public int? UserID { get; set; }                    
            public int? DepartmentID { get; set; }              
            public string? DepartmentName { get; set; }         
            public string? Period { get; set; }                
            public string? FunctionName { get; set; }           
            public int? TotalAvailableHours { get; set; }      
        }
    

}
