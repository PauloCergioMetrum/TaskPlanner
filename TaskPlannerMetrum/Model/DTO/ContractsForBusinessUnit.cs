using DocumentFormat.OpenXml.Presentation;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;

namespace TaskPlannerMetrum.Model.DTO
{
    public class NumberOfContractsForBusinessUnit
    {
        public string BusinessUnit { get; set; }
        public int Value { get; set; }
        public string ProjectInspectorName { get; set; }
        public string TechLeadName { get; set; }
    }
}
