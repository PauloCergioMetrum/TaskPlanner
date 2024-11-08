using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class MilestoneType_HH_Details
    {
        [Key]
        public string MilestoneTypeID { get; set; }
        public string DepartmentName { get; set; }
        public string DisplacementServiceName { get; set; }
        public int DepartmentID { get; set; }
        public string MilestonesValueID { get; set; }
        public int DisplacementServicesID { get; set; }
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public double ValueHour { get; set; }
        public double HoursExpected { get; set; }
        public double HoursPlanned { get; set; }
        public double HoursExecuted { get; set; }
        public double CostHoursExpected { get; set; }
        public double CostHoursPlanned { get; set; }
        public double CostHoursExecuted { get; set; }       
        public int Expr4 { get; set; }
        public double Hours { get; set; }       
        public int MilestonesID { get; set; }
        public double TotalMilesStone { get; set; }
        public int TotalRecordsByContract { get; set; }
        public double ExecutedManHour { get; set; }
        public double PlannedManHour { get; set; }
        public int ID { get; set; }

    }
}
