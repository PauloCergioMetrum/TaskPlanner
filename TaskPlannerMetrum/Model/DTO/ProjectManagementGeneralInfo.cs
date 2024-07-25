using System;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class ProjectManagementGeneralInfo
    {


        public int Id { get; set; }
        public string PredictedSavings { get; set; }

        public string PredictedMarkup { get; set; }

        public DateTime? ValidityStartDate { get; set; }

        public DateTime? ValidityEndDate { get; set; }

        public string Local { get; set; }
        public int RiskLevelID { get; set; }

        public int ConsultantID { get; set; }

        public List<PM_Information_General> ContactClients { get; set; }

       


    }

    
}
