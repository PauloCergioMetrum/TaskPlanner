using System.Collections.Generic;
using System.Security.Claims;
using System;
using TaskPlannerMetrum.Configurations;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Repository.Users;
using TaskPlannerMetrum.Services;
using System.IdentityModel.Tokens.Jwt;
using TaskPlannerMetrum.Model.Context;
using System.Linq;
using TaskPlannerMetrum.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace TaskPlannerMetrum.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private MSSQLContext _context;


        private IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService, MSSQLContext context)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
            _context = context;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            var findUser = _repository.ValidateCredentials(userCredentials);
            if (findUser == null) return null;

            var user = new
            {
                userName = findUser.UserName,
                fullname = findUser.FullName,
                departName = _context.Department.Where(i => i.Id == findUser.DepartmentId).Select(n => n.Name).FirstOrDefault(),
                departmentId = findUser.DepartmentId,
                workspaceID = findUser.WorkspaceID,
                role = findUser.PermissionId.ToString(),
                PermissionId = findUser.PermissionId,
                permissionName = _context.Permissions.Where(i => i.id == findUser.PermissionId).Select(d => d.Description).FirstOrDefault(),
                refreshToken = findUser.RefreshToken,
                refreshTokenExpiryTime = findUser.RefreshTokenExpiryTime,
                creationDate = findUser.CreationDate,
                isActive = findUser.IsActive,
                isDarkMode = findUser.IsDarkMode,
                id = findUser.Id,
                phoneNumber = findUser.PhoneNumber, 
            };
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, findUser.UserName),
                new Claim(ClaimTypes.Role, user.role)
            };


            var accessToken = _tokenService.GenerateAccessToken(claims);
            var token = new JwtSecurityTokenHandler();
            var refreshToken = _tokenService.GenerateRefreshToken();

            findUser.RefreshToken = refreshToken;
            findUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);
            

            _repository.RefreshUserInfo(findUser);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);
            findUser.Password = null;

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken,
                user
                );
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var username = principal.Identity.Name;

            var user = _repository.ValidateCredentials(username);

            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now) return null;


          

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            

            _repository.RefreshUserInfo(user);
            user.Password = null;

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);



            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken,
                user
                );
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}
