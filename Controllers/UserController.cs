using System;
using Microsoft.AspNetCore.Mvc;
using TramWars.Domain;
using TramWars.DTO;
using TramWars.Identity;
using TramWars.Persistence.Repositories.Interfaces;

namespace TramWars.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult Post([FromBody] UserDTO userDTO)
        {
            var appUser = new ApplicationUser(userDTO.Name); 
            var savedUser = userRepository.Add(appUser, userDTO.Password);
            var result = new UserDTO
            {
                Name = savedUser.UserName
            };
            return Created($"/users/{savedUser.UserName}", result);
        }
    }
}