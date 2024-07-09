using System;
using System.Security;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskPlannerMetrum.Model.DTO
{
    public class MilesTonesDTO
    {



        public string ID { get; set; }
        public int Baseline { get; set; }
        public string Name { get; set; }    
        public int MilestonesID {  get; set; }
        public int ContractID { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime RescheduledDate { get; set; }       
        public DateTime? ExecutedDate { get; set; }   
        public int TypeID { get; set; }
        public double Value { get; set; }
        public string? TypeMilestonesID { get; set; }
        public int TechLeadID { get; set; }
        public int BusinessUnitID { get; set; }
    }



}
