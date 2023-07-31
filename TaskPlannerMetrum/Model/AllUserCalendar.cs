using TaskPlannerMetrum.Model.Base;
using System.ComponentModel.DataAnnotations;
using System;

namespace TaskPlannerMetrum.Model
{
    public class AllUserCalendar
    {
        public string DepName { get; set; }
        public int userID { get; set; }
    }
}
