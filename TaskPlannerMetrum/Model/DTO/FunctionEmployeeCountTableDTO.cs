using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.DTO
{
    public class FunctionEmployeeCountTableDTO
    {

        [Key]
        public string FunctionName { get; set; }

        public string DepartmentName { get; set; }
        public int EmployeeCount { get; set; }
       
    }

}


