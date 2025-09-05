using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vMilestonesStatistics
    {

        [Key]
        public int ContractID { get; set; }
        public string ActivityType { get; set; }
        public double TotalPlannedHours { get; set; }
        public double TotalExecutedHours { get; set; }
        public double TotalDeliveredHours { get; set; }

    }
}
