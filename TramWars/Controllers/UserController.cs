using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
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
        [HttpPost]
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

        [Route("{username}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(string username)
        {
            var user = await _users.FindAsync(username);
            var dto = new UserDto
            {
                Name = user.UserName,
                Score = user.Score
            };
            return Ok(dto);
        }

        [Route("{username}")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(string username, string password, [FromBody] UserDto dto)
        {
            var user = await _users.FindAsync(username);
            if (!await _users.CheckPasswordAsync(user, password))
            {
                return Unauthorized();
            }

            user.UserName = dto.Name;
            await _users.UpdateAsync(user);

            if (!string.IsNullOrEmpty(dto.Password))
            {
                await _users.ChangePasswordAsync(user, password, dto.Password);
            }

            return Ok();
        }
    }
}