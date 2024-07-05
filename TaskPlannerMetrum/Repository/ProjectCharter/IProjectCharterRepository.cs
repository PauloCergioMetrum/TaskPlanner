using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.ProjectCharter
{
  public interface IProjectCharterRepository
  {
    public ProjectCharterDatails ProjectCharterDatails(int contractID);
    public List<PM_Information_General> ClientsProjectCharter(int contractID);
  }
}
