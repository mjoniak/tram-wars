using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TramWars.Controllers;
using TramWars.Identity;
using TramWars.Services.Interfaces;
using Xunit;

namespace TramWars.Tests.Controllers
{
    public class TokenControllerTests
    {
        private Mock<IUserService> serviceMock;
        private TokenController controller;

        public TokenControllerTests()
        {
            serviceMock = new Mock<IUserService>();
            controller = new TokenController(serviceMock.Object);
        }

        [Fact]
        public void WhenUserDoesntExistThenBadRequest()
        {            
            var request = new OpenIdConnectRequest { Username = "DoesntExist" };
            serviceMock.Setup(p => p.GetUserAsync(request)).ReturnsAsync(null);

            var result = controller.Post(request).Result;
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void WhenInvalidPasswordThenBadRequest()
        {            
            var user = new ApplicationUser("Exists");
            var request = new OpenIdConnectRequest { Username = user.UserName, Password = "some_pass" };
            serviceMock.Setup(p => p.GetUserAsync(request)).ReturnsAsync(user);
            serviceMock.Setup(p => p.CheckPasswordAsync(user, "some_pass")).ReturnsAsync(false);
            
            var result = controller.Post(request).Result;
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void WhenCorrectRequestThenSignIn()
        {            
            var user = new ApplicationUser("Exists");
            var request = new OpenIdConnectRequest { Username = user.UserName, Password = "some_pass" };
            serviceMock.Setup(p => p.GetUserAsync(request)).ReturnsAsync(user);
            serviceMock.Setup(p => p.CheckPasswordAsync(user, "some_pass")).ReturnsAsync(true);
            serviceMock
                .Setup(p => p.CreateTicketAsync(request, user))
                .ReturnsAsync(new AuthenticationTicket(new ClaimsPrincipal(), new AuthenticationProperties(), "Scheme"));
            
            var result = controller.Post(request).Result;
            Assert.IsType<SignInResult>(result);
        }
    }
}