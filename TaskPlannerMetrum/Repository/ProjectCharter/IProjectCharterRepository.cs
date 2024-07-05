using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.ProjectCharter
{
  public interface IProjectCharterRepository
  {
    public ProjectCharterDatails ProjectCharterDatails(int contractID);
    public List<PM_Information_General> ClientsProjectCharter(int contractID);
    public List<TechLeaderDTO> TechLeadersProjectCharter(int contractID);
    public dynamic Consualtant(int userID);
  }
}
