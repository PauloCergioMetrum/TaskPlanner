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
  }
}
