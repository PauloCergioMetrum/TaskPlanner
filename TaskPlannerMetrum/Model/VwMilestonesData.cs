using Microsoft.EntityFrameworkCore;

namespace TaskPlannerMetrum.Model
{
    [Keyless]
    public class VwMilestonesData
    {
        public int ContractID { get; set; }
        public int MilestonesID { get; set; }
        public string MilestonesName { get; set; } = string.Empty;

        public double HorasPrevistas { get; set; }
        public double HorasPlanejadas { get; set; }
        public double HorasExecutadas { get; set; }

        public double CustoPrevisto { get; set; }
       public decimal CustoPlanejado { get; set; }
        public decimal CustoExecutado { get; set; }

        public string CalculatedMilestoneStatus { get; set; } = string.Empty;
    }


}

