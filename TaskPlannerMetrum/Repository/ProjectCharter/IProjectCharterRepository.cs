using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.ProjectCharter
{
  public interface IProjectCharterRepository
  {
    public ProjectCharterDatails ProjectCharterDatails(int contractID);
  }
}
