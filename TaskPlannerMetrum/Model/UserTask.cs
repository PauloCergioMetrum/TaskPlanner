using Castle.Components.DictionaryAdapter;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace TaskPlannerMetrum.Model
{
    public class UserTask
    {
        [Key]
        public int ID { get; set; }
        public int ActivitiesScopeListID { get; set; }
        public  int UserID { get; set; }
        public int ContractID { get; set; }
        public double Rating { get; set; }

        public int ActivityPlanID { get; set; }        


     
    }
}
