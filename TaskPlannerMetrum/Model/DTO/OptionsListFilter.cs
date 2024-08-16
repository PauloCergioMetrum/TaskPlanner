using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Model.DTO
{
    public class OptionsListFilter
    {
        public List<OptionsFilterTechLead> TechLeader { get; set; }

        public List<OptionsListFilterProjectInspector> ProjectInspector { get; set; }   

        public List<BusinessUnit> BusinessUnits { get; set; }

        public  List<string> Status { get; set; }   
    }
}
