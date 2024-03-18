using System.ComponentModel.DataAnnotations.Schema;

namespace TaskPlannerMetrum.Model
{
    public class PMAcquisitionPlanned
    {
        public string ID { get; set; }
        public int TypeAcquisitionID { get; set; }
        public int AmountPlanned { get; set; }
        public string Description { get; set; }
        public int ContractID { get; set; }
        public double AmountValue { get; set; }
    }
}