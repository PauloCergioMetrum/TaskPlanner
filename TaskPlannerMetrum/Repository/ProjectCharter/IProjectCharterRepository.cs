using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ProjectCharter
{
    public interface IProjectCharterRepository
    {
        public ProjectCharterDatails ProjectCharterDatails(int contractID);
        public List<PM_Information_General> ClientsProjectCharter(int contractID);
        public List<TechLeaderDTO> TechLeadersProjectCharter(int contractID);
        public List<PM_TAP_Resources> Resources(int contractID);
        public dynamic Consualtant(int contractID);
        



    }
}
