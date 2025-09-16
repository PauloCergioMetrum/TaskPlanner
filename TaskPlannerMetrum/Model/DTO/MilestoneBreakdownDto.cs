namespace TaskPlannerMetrum.Model.DTO
{
    public class MilestoneBreakdownDto
    {
        public int ContractID { get; set; }
        public int MilestonesID { get; set; }

        public string MilestoneName { get; set; } = string.Empty;

        // Hours
        public double ExpectedHours { get; set; }    
        public double PlannedHours { get; set; }     
        public double ExecutedHours { get; set; } 
        public double HoursDifference { get; set; }  

        // Costs
        public double ExpectedCost { get; set; }     
        public decimal PlannedCost { get; set; }    
        public decimal ExecutedCost { get; set; }   
        public double CostDifference { get; set; }   

        public string? Status { get; set; }
    }
}
