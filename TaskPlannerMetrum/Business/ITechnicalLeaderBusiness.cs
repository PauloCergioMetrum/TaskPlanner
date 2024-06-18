using System.Collections.Generic;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Business
{
    public interface ITechnicalLeaderBusiness
    {
        public List<UserDto> GetAllTechnicalLeader();
    }
}
