namespace TaskPlannerMetrum.Model.DTO
{
    public class GetContractsByMonthDto
    {

        public string InternalCode { get; set; }
        public string PeriodMonth { get; set; } 
        public string BusinessUnit { get; set; }
        public int ProjectCountPerBusinessUnit { get; set; }
    }
}
