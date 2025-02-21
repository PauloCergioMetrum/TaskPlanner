using System;

namespace TaskPlannerMetrum.Model
{
    public class GetMilestoneNoExpected
    {
        public string FunctionName { get; set; }
        public string DepartmentName { get; set; }
        public string DisplacementServiceName { get; set; }
        public decimal Hours { get; set; }
        public decimal TotalMilestones { get; set; }
        public decimal TotalExecutedManHour { get; set; }
        public decimal TotalPlannedManHour { get; set; }    // <<--- int, mas pode estar vindo decimal
        public decimal CustoPlanejado { get; set; }
        public decimal CustoExecutado { get; set; }

    }



}
