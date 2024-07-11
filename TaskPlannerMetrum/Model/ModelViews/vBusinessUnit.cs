using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class BusinessUnit
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


    }
}
