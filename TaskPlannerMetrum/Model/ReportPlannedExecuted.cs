using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Model
{
    public class ReportPlannedExecuted
    {
        public List<vReports_PlannedExecuted> listPlannedExecuted { get; set; }
        public double totalPlanned { get; set; }
        public double totalExecuted { get; set; }
        public double totalCost {  get; set; }
    }
}
