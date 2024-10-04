using DocumentFormat.OpenXml.Presentation;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class NumberOfContractsForBusinessUnit
    {
        public string label { get; set; }

        public int value { get; set; }
        //public string StartPeriod { get; set; }
        //public string EndPeriod { get; set; }



    }
}
