using Core.ActionFilters;
using Core.BackEnd;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API_Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        [User_UserExistsFilter]
        public IActionResult Login([FromQuery, Required, StringLength(30)] string username, [FromQuery, Required] string password)
        {
            // Realiza el login. El token será nulo si la contraseña es incorrecta.
            string token = new UserSC().Login(username, password);

            if (token != null)
                return Ok(new { Token = token});
            else
                return Unauthorized();
        }

        [HttpPost]
        [User_EnsureNewUserAndEmailActionFilter]
        public IActionResult SingUp([FromBody] UserPostDTO newUser)
        {
            // Da de alta el registro del usuario.
            new UserSC().SignUp(newUser);

            return Created("", "");
        }
    }
}
