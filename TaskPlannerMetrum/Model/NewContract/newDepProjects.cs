using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model.NewContract
{
    public class NewProject
    {

        public DateTime DateRetroactive { get; set; }
        public int ContractID { get; set; }

        public List<DepContracts> DepartmentContract { get; set; }
     
    }
    
    public class DepContracts

    {
        public int DepartmentID { get; set; }

        public int TechLeaderID { get; set; }

        public int ExpectedHour { get; set; }

      



    }
}
