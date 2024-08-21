using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace TaskPlannerMetrum.Model
{
    public class ActivityPlanHHDetail
    {
        public ActivityPlanHH ActivityPlanHH { get; set; }
        public List<ActivityPlanHHTable> ActivityPlanHHTable { get; set; }
        public List<ActivicPlannGrupByMilesTone>? ActivicPlannGrupByMilesTone {  get; set; }    


    }

    public class ActivicPlannGrupByMilesTone
    {
        public int MilestonesID { get; set; }

        public string MilestoneName { get; set; } 

        public List<Function> Functions { get; set; }

        

    }
    public class Function
    {
        public string ExecutorSeniorityLevel { get; set; }
        public double TotalHours { get; set; } 

        public List<Details> Details { get; set; }

        public string MilesTonesName { get; set; }




    }

    public class Details
    {
        public string Executor { get; set; }
        public double PlannedManHour { get; set; }

        public double  ExecutedManHour { get; set; }

        public string BusinesUnit {  get; set; }

        public string SeniorLevel { get; set; }

        public string MilesTonesName { get; set; }
    }
}
