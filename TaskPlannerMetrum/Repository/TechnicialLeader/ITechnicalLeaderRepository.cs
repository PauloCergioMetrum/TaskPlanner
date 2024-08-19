using System.Collections.Generic;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Repository.TechnicialLeader
{
    public interface ITechnicalLeaderRepository
    {



        public List<UserDto> GetAllTechnicalLeader();
    }
}
