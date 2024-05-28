using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vMilestonesItem
    {
        [Key]
        public Guid ID { get; set; }
        public int ContractID { get; set; }

        public string MilestonesName { get; set; }

        public DateTime ScheduledDate { get; set; }

        public DateTime? RescheduledDate { get; set; }

        public DateTime? ExecutedDate { get; set; }

        public string? Description { get; set; }

        public int MilestonesTypeID { get; set; }

    }
}
