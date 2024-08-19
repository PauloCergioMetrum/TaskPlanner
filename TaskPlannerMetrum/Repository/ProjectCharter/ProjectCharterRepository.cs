using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.ProjectCharter
{
  public class ProjectCharterRepository : IProjectCharterRepository
  {
    private readonly MSSQLContext _context;

    public ProjectCharterRepository(MSSQLContext context)
    {
      _context = context;
    }
    public ProjectCharterDatails ProjectCharterDatails(int contractID)
    {
         return _context.GetProjectCharterDatails(contractID);

    }

    public List<PM_Information_General> ClientsProjectCharter(int contractID)
    {
      return _context.PM_Information_General.Where(P => P.ContractID == contractID).ToList();
    }

    public List<TechLeaderDTO> TechLeadersProjectCharter(int contractID)
    {
      return _context.GetTechLeaders(contractID);
    }

    public dynamic Consualtant(int userID)
    {
          return _context.Users.Where( u => u.Id == userID).Select(s => new {s.UserName , s.PhoneNumber, s.UserEmail}).FirstOrDefault();
    }



        public List<PM_TAP_Resources> Resources(int contractID)
        {
         return _context.PM_TAP_Resources.Where(a => a.ContractID == contractID).ToList();
        }
    }
}
