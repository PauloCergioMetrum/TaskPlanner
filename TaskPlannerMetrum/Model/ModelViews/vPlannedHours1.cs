using System;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vPlannedHours1
    {

    
        public Int64 ID { get; set; }
        public int ContractID { get; set; }

        public string ProjectName { get; set; }
        public string ClientName { get; set; }

        public string DepartmentName { get; set; }
        public string TechLeader { get; set; }


        public int StatusID { get; set; }
        public int TechLeaderID { get; set; }
    }
}
