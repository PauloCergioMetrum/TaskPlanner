using System;
using System.Security.Policy;

namespace TaskPlannerMetrum.Model.DTO
{
    public class HoursExecutorDTO
    {
        public string  ExecutorsTeamID { get; set; }    
        public DateTime StartDate { get; set; }  

        public DateTime EndDate { get; set; }   

        public double Hours { get; set; }
        public bool IsOverAllocated { get; set; }
    
    } 
}
