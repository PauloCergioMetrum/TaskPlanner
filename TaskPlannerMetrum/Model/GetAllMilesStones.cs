using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class GetAllMilesStones
    {
        [Key]
        public int ContractID { get; set; }
        public string DepartmentName { get; set; }
        public string DisplacementServiceName { get; set; }
        public string FunctionName { get; set; }
        public double Hours { get; set; }
        public double TotalMilesStone { get; set; }
        public double ExecutedManHour { get; set; }
        public double PlannedManHour { get; set; }
        public double CustoPlanejado { get; set; }
        public double CustoExecutado { get; set; }
    }
}
