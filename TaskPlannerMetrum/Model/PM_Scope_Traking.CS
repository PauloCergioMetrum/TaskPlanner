namespace TaskPlannerMetrum.Model
{
    public class PM_Scope_Traking
    {
        public string ID { get; set; }
        public int ContractID { get; set; }
        public string Item { get; set; }
    
        public bool Remote { get; set; }
        public string Local { get; set; }
        public int ProjectReviewsPlanned { get; set; }
        public int ProjectReviewsMade { get; set; }
        public int PeportReviewsPlanned { get; set; }
        public int PeportReviewsMade { get; set; }
        public int DevelopSystemReviewsPlanned { get; set; }
        public int DevelopSystemReviewsMade { get; set; }
    }
}
