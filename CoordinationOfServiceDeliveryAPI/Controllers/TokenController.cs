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

        [HttpGet("get-token")]
        public IActionResult GetToken([FromHeader] string login, [FromHeader] string password)
        {
            try
            {
                var access_token = _tokenService.GenerateToken(login, password);

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

        [HttpGet("get-me")]
        public UserShort GetMe()
        {
            try
            {
                return _authUserService.GetLoggedUser(HttpContext);
            }
            catch (Exception)
            {
                return new UserShort();
            }
        }
    }
}
