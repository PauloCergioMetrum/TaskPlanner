using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.ProjectManagement
{
    public class ProjectManagementRepository : IProjectManagementRepository
    {
        private readonly MSSQLContext _context;

        public ProjectManagementRepository (MSSQLContext context)
        {
            _context=context;
        }

        public Model.Contracts GetForecastID(int id)
        {
            return _context.Contracts.Where(i => i.id == id).FirstOrDefault();
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
