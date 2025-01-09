using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class Contracts
    {
#nullable enable
        [Key]
        public int id { get; set; } 
        public int inspectorID { get; set; }


        public int TagID { get; set; }

        public DateTime StartDate { get; set; }

        public string?InternalCode { get; set; }

        public int ClientID { get; set; }   

        public string? Condition { get; set; }   

        public string? PaymentMethod { get; set; }

        public int VendorID { get; set;   }

        public bool EnableProject { get; set; }
        public Int64? ClientOrder { get; set; }

        public string? Observation { get; set; }


        public DateTime? DateRetroactive { get; set; }



        public string? PredictedSavings { get; set; }

        public string? PredictedMarkup { get; set; }

        public DateTime? ValidityStartDate { get; set; }

        public DateTime? ValidityEndDate { get; set; }

        public int ?StatusID { get;set; }

        public string? PaymentCondition { get; set; }
        public DateTime? DateFineshed {  get; set; }     

        //public int WorkSpaceID { get; set; }


    }
}
