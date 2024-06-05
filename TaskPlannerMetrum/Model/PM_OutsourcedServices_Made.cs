using System;

namespace TaskPlannerMetrum.Model
{
    public class PM_OutsourcedServices_Made
    {
        public string ID { get; set; }
        public string ID_OutsourcedServices_Planned { get; set; }
        public int Amount { get; set; }
        public double  UnitaryValue { get; set; }
        public double NumberOOC { get; set; }
        public int NumberContract { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
