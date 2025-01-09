namespace TaskPlannerMetrum.Model
{
#nullable enable
    public class MobilizationResult
    {
        public double? AccommodationPlanned { get; set; }
        public double? FoodPlanned { get; set; }
        public double? AirTransportPlanned { get; set; }
        public double? GroundTransportPlanned { get; set; }
        public double? OthersPlanned { get; set; }
        public MobilizationMadeResult? MobilizationMade { get; set; }
    }
}
