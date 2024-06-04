using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vpm_combination_Mobilization
    {
        public string ID { get; set; }

        public double CountMobilization { get; set; }
        public double CountAccommodation { get; set; }
        public double CountFood { get; set; }
        public double CountAirTransport { get; set; }
        public double CountGroundTransport { get; set; }
        public double CountOthers { get; set; }

        public int ContractID { get; set; }

        public double CountMobilizationMade { get; set; }
        public double CountAccommodationMade { get; set; }
        public double CountFoodMade { get; set; }
        public double CountAirTransportMade { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        [StringLength(255)]
        public string IDMade { get; set; }
    }
}
