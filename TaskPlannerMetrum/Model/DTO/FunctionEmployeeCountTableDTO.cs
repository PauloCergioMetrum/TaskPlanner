namespace TaskPlannerMetrum.Model.DTO
{
    public class FunctionEmployeeCountTableDTO
    {


        public string FunctionName { get; set; }

        public string DepartmentName { get; set; }
        public int EmployeeCount { get; set; }
        public double PlannedHours { get; set; }
        public double ExecutedHours { get; set; }
        public int AvailableHours { get; set; }
    }

}


