using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class UserHourCosts
    {
     
        public string ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double HourCost { get; set; }
        public int  UserID { get; set; }

        public int FunctionID { get; set; }

       
    }
}
 