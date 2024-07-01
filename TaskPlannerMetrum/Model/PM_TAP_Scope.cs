namespace TaskPlannerMetrum.Model
{
    public class PM_TAP_Scope
    {
        public int ID { get; set; }
        public int ContractID { get; set; }
        public string Goal { get; set; }
        public string Scope { get; set; }
        public string Premises { get; set; }
        public string Deliveries { get; set; }
        public string GeneralRisks { get; set; }
        public string OutScope{get; set; }
    }
}