using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vPlannedHours
    {
        [Key]
        public Int64 ID { get; set; }
        public int ContractID { get; set; }
       
        public string ProjectName { get; set; }
        public string ClientName { get; set; }

        public string DepartmentName { get; set; }
        public string TechLeader { get; set; }
        public double ExpectedHour { get; set; }
        public double PlannedHour { get; set; }
        public double ExecutedHour { get; set; }
    
        public int StatusID { get; set; }
        public  string StatusGuarantee {  get; set; }  

    }
}
