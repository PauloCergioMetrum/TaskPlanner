using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace TaskPlannerMetrum.Model.DTO
{
    public class OperationalReportReportDTO
    {

        public string StartPeriod { get; set; }
        public string EndPeriod { get; set; }
        public List<int> ContractIDs { get; set; }
        public List<int> TechLeadIDs { get; set; }
        public List<int> ProjectInspectorIDs { get; set; }
        public List<int> BusinessUnitIDs { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public string StatusFilter { get; set; }

        public List<int> InspectorIDs { get; set; }

        public int FiltroStatusID { get; set; }



        public DateTime DateFineshed { get; set; }
        public string InspectorName { get; set; }

        public string BusinessUnit { get; set; }

        public string TechLeaderName { get; set; }

        public int Status { get; set; }


        public double? TotalExecutedHours { get; set; }
        public double? TotalPlannedHours { get; set; }
        public int? TotalAvailableHours { get; set; }



       

        public int InternalCode { get; set; }
        public string ProjectManager { get; set; } 
        public string TechLeader { get; set; } 
        public string ProjectStatus { get; set; } 
        public DateTime? StartDateFilter { get; set; }
        public DateTime? EndDateFilter { get; set; }



    }

    public class PreparetBalancePerProject
    {
        public string BusinessUnit { get; set; }

        public List<BalancePerProject> Details { get; set; } 
    }
}
