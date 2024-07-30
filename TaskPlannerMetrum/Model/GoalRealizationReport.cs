namespace TaskPlannerMetrum.Model
{
    public class GoalRealizationReport
    {
        public int id { get; set; }
        public decimal Target { get; set; }   
        public decimal Realized { get; set; }    
        public decimal Predicted { get; set; }   
    }
}
