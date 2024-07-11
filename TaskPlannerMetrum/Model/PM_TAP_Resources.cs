namespace TaskPlannerMetrum.Model
{
    public class PM_TAP_Resources
    {

        public int Id { get; set; }
        public int ContractID { get; set; }   
        public string Mobilizations { get; set; }     
        public string ExpectedEquipment { get; set; }                
        public string Acquitions { get; set; }
        public string ThirdPartyServices { get; set; }
        public string MobilizationsValue { get; set; }
        public string ExpectedEquipmentValue { get; set; }
        public string AcquitionsValue { get; set; }
        public string ThirdPartyServicesValue { get; set; }
    }
}
