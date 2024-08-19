using Microsoft.Extensions.Primitives;
using System;
using System.ComponentModel.DataAnnotations;
namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vUserList
    {
        [Key]
        public int userid{ get; set; }

        public int PMTeamID{ get; set; }

        public string userName{ get; set; }

        public int DepartmentID { get; set; }

        public int permission_id { get; set;}
    }
}
