using System;

namespace TaskPlannerMetrum.Model
{
    public class PM_Scope_Change
    {

        public string ID { get; set; }
        public int ContractID { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public string Coin { get; set; }
        public int? Value { get; set; } 
        public DateTime? Date { get; set; }
    }
}
