using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vCalendar
    {
        [Key]
        public Int64 ID { get; set; }
        public DateTime ScheduledDate { get; set; }

        public string UserName { get; set; }
     
        public int UserID { get; set; }

        public string UserDepartment { get; set; }

        public string ActivityDescription { get; set; }

        public double PlannedManHour { get; set; }
        
        public int contractID { get; set; }

        public string InternalCode { get;set; }

        public string ActivityDepartment { get; set; }

    }
}
