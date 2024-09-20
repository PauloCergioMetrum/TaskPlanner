using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class TeamAlocationTableDTO
    {
        public string businessUnit { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        
        public List<int> FunctionIds { get; set; } 


        public string project { get; set; }

      
    }
}
