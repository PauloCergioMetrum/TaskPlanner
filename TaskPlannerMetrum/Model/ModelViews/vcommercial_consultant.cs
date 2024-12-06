using System.ComponentModel.DataAnnotations.Schema;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vcommercial_consultant
    {

  
        [Column("InternalCode")]
        public string InternalCode { get; set; }

        [Column("VendorName")]
        public string VendorName { get; set; }

        [Column("ContractID")]
        public int ContractID { get; set; }

        [Column("user_email")]
        public string UserEmail { get; set; }
    }
}
