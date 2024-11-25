using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TaskPlannerMetrum.Model
{
    public class UserHourCosts
    {

        public string ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double HourCost { get; set; }
        public int UserID { get; set; }
        public string FunctionName { get; set; }
        public DateTime CreationDate { get; set; }





    }
}
