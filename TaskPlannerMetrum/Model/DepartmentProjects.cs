using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class DepartmentProjects
    {
        [Key]
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public int FinancesID { get; set; }
        public int TechLeaderID { get; set; }
        public double ExpectedHour { get; set; }
        public int ContractID { get; set; }

     






        
    }
}
