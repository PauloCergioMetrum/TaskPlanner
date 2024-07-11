using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.TechnicialLeader
{
    public class TechnicalLeaderRepository : ITechnicalLeaderRepository
    {
        private MSSQLContext _context;

        public TechnicalLeaderRepository(MSSQLContext context)
        {
            _context = context;
        }

        public List<UserDto> GetAllTechnicalLeader()
        {
        
            return _context.vUserList
                         .Where(user => user.permission_id == 1) 
                         .Select(user => new UserDto
                         {

                             UserID = user.userid,
                             UserName = user.userName ,
                             permission_id = user.permission_id



                         })
                         .ToList();
        }
    }
}
