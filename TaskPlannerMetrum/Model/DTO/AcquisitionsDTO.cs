using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.DTO
{
    public class AcquisitionsDTO
    {
        public int ID{ get; set; }
        public int TypeAcquisitionID { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public int ContractID { get; set; }
    }
}
