using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class Contracts
    {
        [Key]
        public int id { get; set; } 
        public int inspectorID { get; set; }


        public int TagID { get; set; }

        public DateTime StartDate { get; set; }

        public string InternalCode { get; set; }

        public int ClientID { get; set; }   

        public string Condition { get; set; }   

        public string PaymentMethod { get; set; }

        public int VendorID { get; set;   }

        public bool EnableProject { get; set; }
        public Int64 ClientOrder { get; set; }

        public string Observation { get; set; }


        public DateTime? DateRetroactive { get; set; }






    }
}
