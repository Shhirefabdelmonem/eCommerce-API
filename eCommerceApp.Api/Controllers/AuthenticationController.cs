using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.Services.Implementations.Authentication;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(CreateUser user)
        {
            var res = await authenticationService.CreateUser(user);
            return res.Success ? Ok(res) : BadRequest(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult>LoginUser(LoginUser user)
        {
            var res= await authenticationService.LoginUser(user);
            return res.Success? Ok(res) : BadRequest(res) ;
        }

        [HttpGet("refreshToken/{refreshToken}")]
        public async Task<IActionResult>RegenrateToken(string refreshToken)
        {
            var res= await authenticationService.RegenrateToken(refreshToken);
            return res.Success ? Ok(res) : BadRequest(res);
        }

    }
}
