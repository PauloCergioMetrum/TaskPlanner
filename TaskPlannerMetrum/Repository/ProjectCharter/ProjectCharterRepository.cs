using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

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
        public dynamic Consualtant(int contractID)
        {
            var vendorName = _context.vContractList
                .Where(a => a.ContractID == contractID)
                .Select(a => a.VendorName)
                .FirstOrDefault();

            if (vendorName == null)
            {
                throw new Exception($"VendorName não encontrado para ContractID {contractID}");
            }

            var userEmail = _context.Users
                .Where(u => u.UserName == vendorName)
                .Select(u => u.UserEmail)
                .FirstOrDefault();

            return new { VendorName = vendorName, UserEmail = userEmail };
        }


        public List<PM_TAP_Resources> Resources(int contractID)
        {
         return _context.PM_TAP_Resources.Where(a => a.ContractID == contractID).ToList();
        }

        
    }
}
