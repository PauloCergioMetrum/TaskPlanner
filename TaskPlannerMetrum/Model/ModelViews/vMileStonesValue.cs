using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vMileStonesValue
    {
        [Key]
        public string ID { get; set; }
        public int ContractID { get; set; }

        public string MilestonesName { get; set; }

        public DateTime ScheduledDate { get; set; }

        public DateTime? RescheduledDate { get; set; }

        public DateTime? ExecutedDate { get; set; }

        public string? Description { get; set; }

        public int MilestonesTypeID { get; set; }

        public double? Value { get; set; }
        public string TypeMilestonesID { get; set; }
        public int TechLeadID { get; set; }

        public int BusinessUnitID { get; set; }


    }
}
