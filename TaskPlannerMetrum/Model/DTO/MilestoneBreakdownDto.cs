namespace TaskPlannerMetrum.Model.DTO
{
    public class MilestoneBreakdownDto
    {
        public int ContractID { get; set; }
        public int MilestonesID { get; set; }
        public string Marco { get; set; } = string.Empty;

        public double HorasPrevistas { get; set; }
        public double HorasPlanejadas { get; set; }
        public double HorasExecutadas { get; set; }
        public double DiferencaHoras { get; set; }

        public double CustoPrevisto { get; set; }
        public decimal CustoPlanejado { get; set; }
        public decimal CustoExecutado { get; set; }
        public double DiferencaCustos { get; set; }

        public string? Status { get; set; }
    }
}
