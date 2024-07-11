using System.ComponentModel.DataAnnotations;
using TaskPlannerMetrum.Business;

namespace TaskPlannerMetrum.Model
{
    public class MilestonesItem
    {
        [Key] 
        public int ID { get; set; }   
        public int ContractID { get; set; }

        public string Name { get; set; }
       


    }
}
