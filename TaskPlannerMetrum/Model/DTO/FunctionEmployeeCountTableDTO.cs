namespace TaskPlannerMetrum.Model.DTO
{
    public class FunctionEmployeeCountTableDTO
    {
        public string FunctionName { get; set; }
        public string DepartmentID { get; set; }      
        public string DepartmentName { get; set; }
        public string UserID { get; set; }             
        public int EmployeeCount { get; set; }
        public decimal Percentage { get; set; }
    }

}
