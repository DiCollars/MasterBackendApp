using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Models;
using ServicesContracts.ServiceInterfaces;
using System; 

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthUserService _authUserService;

        public TokenController(ITokenService tokenService, IAuthUserService authUserService)
        {
            _tokenService = tokenService;
            _authUserService = authUserService;
        }

        [HttpPost("get-token")]
        public IActionResult GetToken([FromBody] UserAuth userShort)
        {
            try
            {
                var access_token = _tokenService.GenerateToken(userShort.Login, userShort.Password);

                if (access_token == null)
                {
                    throw new Exception("Token is null.");
                }

                return Json(access_token);
            }
            catch (Exception)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
        }

        [Authorize(Roles = "ADMIN, MASTER, CLIENT")]
        [HttpGet("get-me")]
        public UserFull GetMe()
        {
            try
            {
                return _authUserService.GetLoggedUserFull(HttpContext);
            }
            catch (Exception)
            {
                return new UserFull();
            }
        }
    }
}
