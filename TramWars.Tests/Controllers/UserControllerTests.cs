using Microsoft.AspNetCore.Mvc;
using Moq;
using TramWars.Controllers;
using TramWars.DTO;
using TramWars.Identity;
using TramWars.Persistence.Repositories.Interfaces;
using Xunit;

namespace TramWars.Tests.Controllers
{
    public class UserControllerTests
    {
        private const string UserName = "user_name123";
        private const string Password = "pass123";
        private UserDTO dto;
        private Mock<IUserRepository> repositoryMock;
        private UserController controller;

        public UserControllerTests()
        {
            dto = new UserDTO 
            {  
                Name = UserName,
                Password = Password
            };
            var user = new ApplicationUser(UserName);
            repositoryMock = new Mock<IUserRepository>(); 
            repositoryMock
                .Setup(p => p.Add(It.Is<ApplicationUser>(u => u.UserName == UserName), Password))
                .Returns(user)
                .Verifiable();
            controller = new UserController(repositoryMock.Object);
        }

        [Fact]
        public void WhenAllFieldsPresentPostUserReturnsUser()
        {
            var result = controller.Post(dto) as CreatedResult;
            var returnedUser = result.Value as UserDTO;
            Assert.Equal(UserName, returnedUser.Name);
            Assert.Null(returnedUser.Password);
        }

        [Fact]
        public void WhenAllFieldsPresentPostUserCreatesUser()
        {
            controller.Post(dto);
            repositoryMock.Verify();
        }
    }
}