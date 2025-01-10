using Microsoft.AspNetCore.Mvc.Versioning;
using System;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ReportPlannedExecutedDTO
    {
        public DateTime? startDate { get; set; } 
        public DateTime? endDate { get; set; }   

        public List<int> lContractID { get; set; }

        public List<int> lExecutedID { get; set; }

        public List<int> lInspectorID { get; set; }

        public List<int> lTechLeaderID { get; set; }

    }
}
