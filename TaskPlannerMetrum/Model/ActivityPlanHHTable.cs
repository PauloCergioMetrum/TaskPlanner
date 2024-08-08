namespace TaskPlannerMetrum.Model
{
    public class ActivityPlanHHTable
    {
        public string BusinessUnit { get; set; }
        public string MilestoneName { get; set; }
        public string ExecutorUserName { get; set; }
        public string ExecutorSeniorityLevel { get; set; }
        public double PlannedManHour { get; set; }
        public double ExecutedManHour { get; set; }
        public double TotalHours { get; set; }   
    }
}
