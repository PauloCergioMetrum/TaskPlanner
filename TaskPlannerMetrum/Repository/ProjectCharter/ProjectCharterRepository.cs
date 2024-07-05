using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
      var sql = "[dbo].[GetProjectCharterDetails] @ContractID"; 
      var projectCharterDetailsList = _context.Set<ProjectCharterDatails>()
          .FromSqlRaw(sql, new SqlParameter("@ContractID", contractID)).FirstOrDefault();         

      return projectCharterDetailsList;
    }
  }
}
