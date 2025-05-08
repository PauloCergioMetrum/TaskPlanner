using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.DTO
{
    namespace TaskPlannerMetrum.Model.DTO
    {
        public class TeamAllocationReportRequestDTO
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public List<string> FunctionName { get; set; }  
            public string DepartmentName { get; set; }
        }
    }
}
