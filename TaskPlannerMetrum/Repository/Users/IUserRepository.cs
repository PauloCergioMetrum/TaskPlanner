using System.Collections.Generic;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TaskPlannerMetrum.Model.ModelViews;
using TaskPlannerMetrum.Model.DTO;

namespace TaskPlannerMetrum.Repository.Users
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);

        User ValidateCredentials(string username);

        bool RevokeToken(string username);

        User RefreshUserInfo(User user);

        public List<UserViewDto> FindAll();

        public  string sha256(string randomString);

        public int Create(User user);

        public bool CreateTeam(Team id);

        public string ComputeHash(string input, SHA256CryptoServiceProvider algorithm);

        public bool isDarkMode(int id);

        public bool IsActiveDarkMode(int id);


        public dynamic GetAllUsers();


    }
}
