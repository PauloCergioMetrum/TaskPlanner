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
    public dynamic ProjectCharterDatails(int contractID)
    {
      var tapInfo = _projectCharterRepository.ProjectCharterDatails(contractID);    
      var clients = _projectCharterRepository.ClientsProjectCharter(contractID);

      var tap = new
      {
        tapInfo,
        Clients = clients
      };

      return tap;
    }
  }
}
