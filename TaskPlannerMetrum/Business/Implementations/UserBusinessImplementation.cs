using System;
using System.Collections.Generic;
using TaskPlannerMetrum.Data.Converter.Implementations;

using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Repository.Generic;
using TaskPlannerMetrum.Repository.Users;
using System.Security.Cryptography;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class UserBusinessImplementation : IUserBusiness
    {
        private readonly IRepository<User> _repository;
        private readonly IUserRepository _userRepository;




        private readonly UserConverter _converter;

        public UserBusinessImplementation(IRepository<User> repository, IUserRepository userRepository)
        {
            _userRepository= userRepository;
            _repository = repository;
            _converter = new UserConverter();
        }

        // Method responsible for returning all people,
        public List<UserVO> FindAll()
        {
            return _userRepository.FindAll();
        }



        // Method responsible for returning one person by ID
        public UserVO FindByID(int id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        // Method responsible to crete one new person
        public bool Create(TeamUsers user)
        {

            var userId = _userRepository.Create(new User
            {
                CreationDate = DateTime.Now,
                DepartmentId = user.DepartmentId,
                FullName = user.FullName,
                UserName = user.UserName,
                Password = _userRepository.ComputeHash("123456", new SHA256CryptoServiceProvider()),
                PhoneNumber = user.PhoneNumber,
                PermissionId = user.PermissionId,
                WorkspaceID = user.WorkspaceID,
                UserEmail = user.UserEmail,
                IsActive = true

            });
            if (userId != null)
            {
                return _userRepository.CreateTeam(new Team
                {
                    DepartmentID = user.DepartmentId,
                    HoursAvailability = 8,
                    isLeader = user.isLeader,
                    UserID = userId,
                    SeniorityLevel = user.SeniorityLevel,
                    WorkForceType = user.WorkForceType,
                    WorkForceClass = user.WorkForceClass,
                    ManHourCost = 15,

                });

            }

            return false;
        }



        // Method responsible for updating one person
        public UserVO Update(UserVO user)
        {

            var userEntity = _converter.Parse(user);
            var userPassword = _repository.FindByID(user.Id);
            userEntity.IsActive = user.IsActive;
            userEntity.Password = userPassword.Password;
            userEntity.UserEmail = user.UserEmail;
            userEntity.UserName = user.UserName;
            userEntity.PermissionId = user.PermissionId;
            //userEntity.PermissionName = user.PermissionName;    



            userEntity = _repository.Update(userEntity);
            return _converter.Parse(userEntity);
        }
        public bool ChangePassowrd(UserVO user)
        {
            try
            {
                var userEntity = _repository.FindByID(user.Id);
                userEntity.Password = _userRepository.ComputeHash(user.Password, new SHA256CryptoServiceProvider());
                _repository.Update(userEntity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        // Method responsible for deleting a person from an ID
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        UserVO IUserBusiness.FindByID(int id)
        {
            throw new System.NotImplementedException();
        }


      

        public bool isDarkMode(int id)
        {
            return _userRepository.isDarkMode(id);
        }

        public bool IsActiveDarkMode(int id)
        {
            return _userRepository.IsActiveDarkMode(id);    
        }

        public dynamic GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
    }
}
