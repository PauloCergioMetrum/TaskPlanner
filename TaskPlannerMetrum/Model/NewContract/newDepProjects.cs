using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.NewContract
{
    public class NewProject
    {
        internal DateTime RetroactiveDate;

        public int ContractID { get; set; }

        public List<DepContracts> DepartmentContract { get; set; }
     
    }
    
    public class DepContracts
    {
        public int DepartmentID { get; set; }

        public int TechLeaderID { get; set; }

        public int ExpectedHour { get; set; }

        public DateTime RetroactiveDate { get; set; }
    }
}
