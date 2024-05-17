using Castle.Components.DictionaryAdapter;
using System;
using Microsoft.VisualBasic;

using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vContractProject
    {
        public int id { get; set; }
        public string ClientName { get; set; }
        public string InternalCode { get; set; }
        public int ClientID { get; set; }
        public string InspectorName { get; set; }
        public bool EnableProject { get; set; }
        public DateTime StartDate { get; set; }

        public string Expectedhour { get; set; }

        public string PlannedMenHour { get; set; }

        public string ExecutedMenHour { get; set; }

        public string Progress { get; set; }

        public DateTime? DateRetroactive { get; set; }

        public int Delayed { get; set; }

        public int Status {  get; set; }



    }
}
