using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vPM_Mobilization_Combined
    {
        public string ID { get; set; }

        public double CountMobilization { get; set; }
        public double CountAccommodation { get; set; }
        public double CountFood { get; set; }
        public double CountAirTransport { get; set; }
        public double CountGroundTransport { get; set; }
        public double CountOthers { get; set; }

        public int ContractID { get; set; }

        public double TotalMade { get; set; }
        public double TotalPlanned { get; set; }
        public double Difference { get; set; }
        public double TotalOfEverything {  get; set; }  

    }
}
