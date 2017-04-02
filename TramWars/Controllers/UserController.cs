using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TramWars.Domain;
using TramWars.Dto;

namespace TramWars.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUsersFacade _users;

        public UserController(IUsersFacade users)
        {
            _users = users;
        }

        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            var appUser = new AppUser(userDto.Name);
            var result = await _users.CreateAsync(appUser, userDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Created($"/users/{appUser.UserName}", result);
        }
    }
}