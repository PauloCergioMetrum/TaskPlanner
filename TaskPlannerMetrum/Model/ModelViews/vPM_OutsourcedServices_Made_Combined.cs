using System;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vPM_OutsourcedServices_Made_Combined
    {

        public string ID { get; set; }
        public string ID_OutsourcedServices_Planned { get; set; }
        public int Amount { get; set; }
        public double UnitaryValue { get; set; }
        public double NumberOOC { get; set; }
        public int NumberContract { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public double TotalOutsourcedServices { get; set; }
    }
}
