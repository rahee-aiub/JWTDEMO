using JWTDEMO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTDEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromForm] AuthenticationRequest authenticationrequest)
        {
            var jwtAuthenticationManager = new JwtAuthenticationManager();
            var authResult = jwtAuthenticationManager.Authincate(authenticationrequest.UserName, authenticationrequest.Password);
            if (authResult == null)
                return Unauthorized();
            else
                return Ok(authResult);
        }
    }
}
