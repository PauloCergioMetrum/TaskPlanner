using System.Linq;
using System.Text;
using System;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Model.Context;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Data;
using System.Collections.Immutable;
using TaskPlannerMetrum.Model.ModelViews;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml.Utils;
using TaskPlannerMetrum.Model.DTO;
using DocumentFormat.OpenXml.Math;

namespace TaskPlannerMetrum.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly MSSQLContext _context;

        public UserRepository(MSSQLContext context)
        {
            _context = context;
        }
        public User ValidateCredentials(UserVO user)
        {
            using (var algorithm = SHA256.Create())
            {
                var pass = ComputeHash(user.Password, algorithm);
                var ActiveUser = _context.Users.FirstOrDefault(u => (u.UserEmail == user.UserEmail) && (u.Password == pass));
                if (ActiveUser != null && ActiveUser.IsActive == true)
                {
                    return ActiveUser;
                }
            }
            return null;
        }
        public User ValidateCredentials(string userName)
        {

            return _context.Users.SingleOrDefault(u => (u.UserName == userName));
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u => (u.UserName == userName));
            if (user is null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

       
        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public List<UserViewDto> FindAll()
        {
            var users = _context.vUsersView.ToList();

            var userList = users.Select(u => new UserViewDto
            {
                Id = u.Id,
                UserName = u.UserName,
                FullName = u.FullName,
                UserEmail = u.UserEmail,
                DepartmentId = u.DepartmentId,
                DepartamentName = u.DepartamentName,
                Role = u.Role.ToString(),
                PermissionId = u.PermissionID,
                PhoneNumber = u.PhoneNumber,
                WorkspaceID = u.WorkspaceID,
                IsActive=   u.IsActive,
                CreationDate = u.CreationDate,
                PermissionName = u.PermissionName,
                FunctionName = u.FunctionName,
                ManagementID = u.ManagementID,
                ManagementName =u.ManagementName
            }).ToList();
            return userList;
          
        }
        public string GetDepartament(int id)
        {
            var departamento = _context.Department.Where(d => d.ID == id).Select(n => n.Name).FirstOrDefault().ToString();
            return departamento;
        }
        public string sha256(string randomString)
        {
            using (var crypt = SHA256.Create())
            {
                string hash = string.Empty;
                byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
                foreach (byte theByte in crypto)
                {
                    hash += theByte.ToString("x2");
                }
                return hash;
            }
        }
        public int Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();




            return _context.Users.Where(u => u.UserEmail == user.UserEmail).Select(u => u.Id).FirstOrDefault();

        }

        public bool CreateTeam(Team team)
        {
            try
            {
                _context.Team.Add(team);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool isDarkMode(int id)
        {
            var update = _context.Users.Where(i => i.Id == id).FirstOrDefault();
            try
            {
                if (update.IsDarkMode == false)
                {
                    update.IsDarkMode = true;
                    _context.Update(update);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    update.IsDarkMode = false;
                    _context.Update(update);
                    _context.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsActiveDarkMode(int id)
        {
            return _context.Users.Where(i => i.Id == id).Select(i => i.IsDarkMode).FirstOrDefault();
        }


        public dynamic GetAllUsers()
        {

            var users = _context.Users.ToList();



            return users.Select(u => new
            {
                u.UserName,
                Id = _context.Team.Where(i => i.UserID == u.Id).Select(i => i.ID).FirstOrDefault(),
                u.PhoneNumber,
                u.UserEmail,
                u.WorkspaceID,
                u.DepartmentId,
                u.FullName,
                u.CreationDate,
                u.IsActive,
            }).ToList();
        }







    }
}
