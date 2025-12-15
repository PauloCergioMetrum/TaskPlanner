using System;

namespace TaskPlannerMetrum.Model

{
    public class PmCostMade
    {
        public string Id { get; set; }
      
        public int Amount { get; set; }
        public double ValueUnit { get; set; }
        public string Description { get; set; }

        public DateTime? DatePerformed { get; set; }

        public string Pm_Cost_Planned_Id { get; set; }
    }
}
