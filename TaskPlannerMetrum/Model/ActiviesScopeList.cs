using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class ActiviesScopeList
    {
        [Key]
        public int ID { get; set; }

        public string Description { get; set; }

        public string SeniorityLevelNeed { get; set; }

        public int GroupTaskID { get; set; }

    }
}
