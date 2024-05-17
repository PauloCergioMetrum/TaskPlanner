using Memt.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TaskPlannerMetrum.Business;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;

namespace TaskPlannerMetrum.Controllers
{
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        [AllowAnonymous]
        public IActionResult Signin([FromBody] UserVO user)
        {
            try
            {
                if (user == null) return BadRequest("Ivalid client request");
                var token = _loginBusiness.ValidateCredentials(user);
                if (token == null) return Unauthorized();
                return Ok(token);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex);
            };
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            try
            {
                if (tokenVo is null) return BadRequest("Ivalid client request");
                var token = _loginBusiness.ValidateCredentials(tokenVo);
                if (token == null) return BadRequest("Ivalid client request");
                return Ok(token);
            }catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);

                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            try
            {
                var username = User.Identity.Name;
                var result = _loginBusiness.RevokeToken(username);

                if (!result) return BadRequest("Ivalid client request");
                return NoContent();
            }catch (Exception ex)
            {
                Logger.Log(ex.Message, ELoggerType.Debug);
                return BadRequest(ex);
            }
        }
    }
}
