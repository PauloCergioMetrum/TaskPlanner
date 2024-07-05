using System;
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
      var InfoGeneral = _projectCharterRepository.ProjectCharterDatails(contractID);    
      var contactClients = _projectCharterRepository.ClientsProjectCharter(contractID);
      var techLeaders =  _projectCharterRepository.TechLeadersProjectCharter(contractID);
      var consultant = _projectCharterRepository.Consualtant(Convert.ToInt32(InfoGeneral.ConsultantID));
      var tap = new
      {
        InfoGeneral,
        Clients = contactClients,
        TechLeaders = techLeaders,
        Consultant = consultant
      };

      return tap;
    }
  }
}
