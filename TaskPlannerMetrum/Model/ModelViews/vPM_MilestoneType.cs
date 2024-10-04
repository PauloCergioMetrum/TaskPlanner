using DocumentFormat.OpenXml.Office.CoverPageProps;
using System.Data;

namespace TaskPlannerMetrum.Model.ModelViews
{
    public class vPM_MilestoneType
    {
        public string DepartmentName { get; set; }
        public string DisplacementServiceName { get; set; }
        public int DepartmentID { get; set; }
        public string MilestonesValueID { get; set; }
        public int DisplacementServicesID { get; set; }
        public double Hours { get; set; }
        public string ID { get; set; }
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }

        public double ValueHour { get; set; }
        public double? TotalMilesStone { get; set; }
        public int MilestonesID { get; set; }
        public int TotalRecordsByContract { get; set; }
    }
}