using System;
using System.Security;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskPlannerMetrum.Model.DTO
{
    public class MilesTonesDTO
    {
        public int ID { get; set; }
        public int ContractID { get; set; }     
        public string March { get; set; }

        public DateTime PlannedDate { get; set; }

        public DateTime ReplannedDate { get; set; }

        public int Baseline { get; set; }   

        public DateTime DatePerformed { get; set; }

        public string Detail { get; set; }  
    }
}
