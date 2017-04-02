using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TramWars.Controllers;
using TramWars.Domain;
using TramWars.Dto;
using Xunit;

namespace TramWars.Tests.Controllers
{
    public class UserControllerTests
    {
        private const string UserName = "user_name123";
        private const string Password = "pass123";
        private readonly UserDto _dto;
        private readonly Mock<IUsersFacade> _users;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _dto = new UserDto
            {
                Name = UserName,
                Password = Password
            };
            _users = new Mock<IUsersFacade>();
            _users
                .Setup(p => p.CreateAsync(It.Is<AppUser>(u => u.UserName == UserName), Password))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();
            _controller = new UserController(_users.Object);
        }

        [Fact]
        public async Task WhenAllFieldsPresentPostUserReturnsUser()
        {
            var result = await _controller.Post(_dto) as CreatedResult;

            Assert.NotNull(result);
            Assert.Equal($"/users/{UserName}", result.Location);
        }

        [Fact]
        public async Task WhenAllFieldsPresentPostUserCreatesUser()
        {
            await _controller.Post(_dto);
            _users.Verify();
        }
    }
}