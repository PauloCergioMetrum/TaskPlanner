using DocumentFormat.OpenXml.Office2010.Excel;

namespace TaskPlannerMetrum.Model
{
    public class ActivityPlanHHTable
    {
        public int ID { get; set; }
        public string BusinessUnit { get; set; }
        public string MilestoneName { get; set; }
        public string ExecutorUserName { get; set; }
        public string ExecutorSeniorityLevel { get; set; }
        public double PlannedManHour { get; set; }
        public double ExecutedManHour { get; set; }
        public double TotalHours { get; set; }   
    }
}
