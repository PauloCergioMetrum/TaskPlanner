using System;
using System.ComponentModel.DataAnnotations;
namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vActivePlanBusinessUnit
    {
        [Key]
        public string BusinessUnit { get; set; }
        public int ContractID { get; set; }
    }
}
