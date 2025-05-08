using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace TaskPlannerMetrum.Model.DTO
{
    public class GetProjectExecutiveStatusDto
    {
        [Key]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string>? InternalCode { get; set; } 
        public List<string>? BusinessUnit { get; set; } 
        public List<string>? InspectorName { get; set; } 
        public List<string>? ContractInspector { get; set; }

    }
}

