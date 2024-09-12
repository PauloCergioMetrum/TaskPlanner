namespace TaskPlannerMetrum.Model.DTO
{
    public class Milestone
    {
        public int ContractID { get; set; }
        public int? MilestonesID { get; set; }
        public string MilestoneName { get; set; }
        public int? TechLeadID { get; set; } 
        public string UserName { get; set; }
        public int? BusinessUnitID { get; set; } 
        public string BusinessUnit { get; set; }
        public int? Delayed { get; set; }
    }


}
