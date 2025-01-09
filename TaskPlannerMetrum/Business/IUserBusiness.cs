using System.Collections.Generic;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.DTO;
using TaskPlannerMetrum.Model.ModelViews;

namespace TaskPlannerMetrum.Business
{
    public interface IUserBusiness
    {
        bool Create(TeamUsers person);
        UserVO FindByID(int id);
        public List<UserViewDto> FindAll();


        UserVO Update(UserVO person);
        void Delete(int id);
        bool ChangePassowrd(UserVO user);
  

        public bool isDarkMode(int id);

        public bool IsActiveDarkMode(int id);

        public dynamic GetAllUsers();


    }
}
