using System.ComponentModel.DataAnnotations.Schema;

namespace TaskPlannerMetrum.Model
{
    public class PMAcquisitionPlanned
    {
        public int ID { get; set; }
        public int TypeAcquisitionID { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public int ContractID { get; set; }
    }
}
