namespace TaskPlannerMetrum.Model
{
    public class PM_Man_Hours
    {
        public string ID { get; set; } 
        public string Departmemt {  get; set; } 
        public int FunctionHH {  get; set; }    

        public int Type {  get; set; }  

        public double QuantityHH { get; set; }

        public int ContractID { get; set; } 
    }
}
