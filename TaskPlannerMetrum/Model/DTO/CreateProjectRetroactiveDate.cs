using System;
using System.Collections.Generic;

namespace TaskPlannerMetrum.Model.DTO
{
    public class CreateProjectRetroactiveDate
    {

        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public int FinancesID { get; set; }
        public int TechLeaderID { get; set; }
        public double ExpectedHour { get; set; }
        public int ContractID { get; set; }

        public DateTime? RetroactiveDate { get; set; }


    }
}
