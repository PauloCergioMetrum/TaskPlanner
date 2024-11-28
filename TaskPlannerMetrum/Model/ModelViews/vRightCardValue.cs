namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vRightCardValue
    {
        public int ContractID { get; set; }                  
        public double TotalValue { get; set; }              
        public double TotalInvoicedValue { get; set; }      
        public double TotalCostDifference { get; set; }      
        public double TotalCostHoursExecuted { get; set; }  
        public string PredictedMarkup { get; set; }         
        public double TotalCostHoursExecutedPercentage { get; set; }
    }
    }
