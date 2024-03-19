using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.DTO
{
    public class AcquisitionsDTO
    {
        public string ID{ get; set; }
        public int TypeAcquisitionID { get; set; }
        public int? AmountPlanned { get; set; }
        public string? Description { get; set; }
        public int ContractID { get; set; }
        public double? AmountValue { get; set; }
    }
}
