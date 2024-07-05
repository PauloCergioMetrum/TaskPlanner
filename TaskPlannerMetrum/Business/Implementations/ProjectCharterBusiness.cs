using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Repository.ActiviesScope;
using TaskPlannerMetrum.Repository.ProjectCharter;

namespace TaskPlannerMetrum.Business.Implementations
{
  public class ProjectCharterBusiness : IProjectCharterBusiness
  {
    private readonly IProjectCharterRepository _projectCharterRepository;

    public ProjectCharterBusiness(IProjectCharterRepository projectCharterRepository)
    {
      _projectCharterRepository = projectCharterRepository;
    }
    public ProjectCharterDatails ProjectCharterDatails(int contractID)
    {
      return _projectCharterRepository.ProjectCharterDatails(contractID);
    }
  }
}
