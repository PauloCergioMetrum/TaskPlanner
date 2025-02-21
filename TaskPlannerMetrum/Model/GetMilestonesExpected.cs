using DocumentFormat.OpenXml.Math;

namespace TaskPlannerMetrum.Model
{
    public class GetMilestonesExpected
    {
        public string FunctionName { get; set; }
        public string DepartmentName { get; set; }
        public string DisplacementServiceName { get; set; }
        public decimal Hours { get; set; }
        public decimal TotalMilestones { get; set; }
        public decimal TotalExecutedManHour { get; set; }      
        public decimal TotalPlannedManHour { get; set; }   
        public decimal CustoPlanejado { get; set; }
        public decimal CustoExecutado { get; set; }
        public decimal ValueHour { get; set; }


        
    }
}




