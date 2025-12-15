using System;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vPm_Cost_Planned
    {
        public string Id { get; set; }
        public int TypeID { get; set; }
        public int Amount { get; set; }
        public double ValueUnit { get; set; }
        public string Description { get; set; }
        public int ContractID { get; set; }
        public DateTime? DatePerformed { get; set; }
        public double? Total {  get; set; }  
        public double TotalPlanned { get; set; }  
        public double? Difference {  get; set; } 

    }
}
