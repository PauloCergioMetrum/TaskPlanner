namespace TaskPlannerMetrum.Model
{
    public class FinalizedStatusDto
    {
        public int ContractID { get; set; }
        public int MilestoneID { get; set; }
        public bool IsFinalized { get; set; }
        public string FinalizedBy { get; set; } 
    }
}
