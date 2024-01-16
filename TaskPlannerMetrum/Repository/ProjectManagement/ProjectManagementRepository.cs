using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.ProjectManagement
{
    public class ProjectManagementRepository : IProjectManagementRepository
    {
        private readonly MSSQLContext _context;

        public ProjectManagementRepository (MSSQLContext context)
        {
            _context=context;
        }

        public int CreateMilestonesItem(MilestonesItem milestones)
        {
            try
            {
                var existMilesTones = _context.MilestonesItem.Where(n => n.Name == milestones.Name && n.ContractID == milestones.ContractID).FirstOrDefault();
                if(existMilesTones == null)
                {
                    _context.MilestonesItem.Add(milestones);
                    _context.SaveChanges();
                    return _context.MilestonesItem.Where(n => n.Name == milestones.Name && n.ContractID == milestones.ContractID).Select(i => i.ID).FirstOrDefault();
                }
                else
                {
                    return 0; 
                }
                
            }
            catch
            {
                return 0;
            }
        }

        public bool CreateMilestonesValue(MilestonesValue milestones)
        {
            try
            {
                _context.MilestonesValue.Add(milestones);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;   
            }
        }

        public Model.Contracts GetForecastByID(int id)
        {

            return _context.Contracts.Where(i => i.id == id).FirstOrDefault();
        }

        public vContractList GetForecastByIDView(int id)
        {
            return _context.vContractList.Where(i => i.ContractID == id).FirstOrDefault();  
        }

        public bool UpdateForecast(Model.Contracts forecast)
        {
            try
            {


                _context.Update(forecast);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

       
    }
}
