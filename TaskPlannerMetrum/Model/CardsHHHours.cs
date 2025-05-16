using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class CardsHHHours
    {
        [Key]
        public int ContractID { get; set; }

        public double? ForecastedHours { get; set; }

        public double? PlannedHours { get; set; }

        public double? ExecutedHours { get; set; }

        public double? ForecastedCost { get; set; }

        public double? PlannedCost { get; set; }

        public double? ExecutedCost { get; set; }
    }
}
