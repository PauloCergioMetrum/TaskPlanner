using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class ProjectTechnicialLeader : IProjectTechnicialLeaderBussines
    {
        private readonly MSSQLContext _context;

        public ProjectTechnicialLeader(MSSQLContext context)
        {
            _context = context;
        }


        public List<string> GetTechLeadersForMatchingProjects()
        {
            var techLeaders = _context.vPlannedHours
                .Select(plannedHour => plannedHour.TechLeader)
                .Distinct()
                .ToList();

            return techLeaders;
        }

        public List<vPlannedHours> GetProjectsByTechLeader(int techLeaderName)
        {
            var projects = _context.vPlannedHours
                .Where(p => p.TechLeaderID == techLeaderName)
 
                .ToList();

            return projects;
        }
    }
}
