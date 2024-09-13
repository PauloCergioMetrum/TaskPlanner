namespace TaskPlannerMetrum.Model
{
    public class MilestoneEntity
    {
        public int ContractID { get; set; }
        public int MilestonesID { get; set; }
        public string MilestoneName { get; set; }
        public int? TechLeadID { get; set; }
        public string TechLeadName { get; set; }
        public int? BusinessUnitID { get; set; }
        public string BusinessUnit { get; set; }
        public int? Delayed { get; set; }
    }
}
