using System.Collections.Generic;
using System.Linq;
using TaskPlannerMetrum.Data.Converter.Contract;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Data.Converter.Implementations
{
    public class UserConverter : IParser<UserVO, User>, IParser<User, UserVO>
    {
        public User Parse(UserVO origin)
        {
            if (origin == null) return null;
            return new User
            {
                Id = origin.Id,
                UserName = origin.UserName,
                FullName = origin.FullName,
                UserEmail=  origin.UserEmail,
                DepartmentId = origin.DepartmentId,
                PermissionId = origin.PermissionId,
                PhoneNumber = origin.PhoneNumber,
                Password= origin.Password,
                WorkspaceID = origin.WorkspaceID,
                CreationDate = origin.CreationDate,
            };
        }

        public List<User> Parse(List<UserVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public UserVO Parse(User origin)
        {
            return new UserVO
            {
                Id = origin.Id,
                UserName = origin.UserName,
                FullName = origin.FullName,
                UserEmail = origin.UserEmail,
                DepartmentId = origin.DepartmentId,
                PermissionId = origin.PermissionId,
                PhoneNumber = origin.PhoneNumber,
                Password = origin.Password,
                WorkspaceID= origin.WorkspaceID,
                CreationDate= origin.CreationDate,
            };
        }

        public List<UserVO> Parse(List<User> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
