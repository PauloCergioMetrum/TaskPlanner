using System.Collections.Generic;

using System.Collections.Generic;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Business
{
    public interface IProjectTechnicialLeaderBussines
    {
        List<string> GetTechLeadersForMatchingProjects();
        public List<vPlannedHours> GetProjectsByTechLeader(int techLeaderID);
    }
}
