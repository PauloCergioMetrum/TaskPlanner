namespace TaskPlannerMetrum.Model.DTO
{
    public class GetContractsByMonthDto
    {
        public string PeriodMonth { get; set; } // Ex: "2025-04"
        public string BusinessUnit { get; set; }
        public int ProjectCountPerBusinessUnit { get; set; }
    }
}
