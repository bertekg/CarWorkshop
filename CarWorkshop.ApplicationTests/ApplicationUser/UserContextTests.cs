using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using Xunit;

namespace CarWorkshop.Application.ApplicationUser.Tests;

public class UserContextTests
{
    [Fact()]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Email, "test@tests.com"),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(ClaimTypes.Role, "User")
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });
        UserContext userContext = new UserContext(httpContextAccessorMock.Object);

        // Act
        CurrentUser? currentUser = userContext.GetCurrentUser();

        // Arrange
        currentUser.Should().NotBeNull();
        currentUser!.Id.Should().Be("1");
        currentUser.Email.Should().Be("test@tests.com");
        currentUser.Roles.Should().ContainInOrder("Admin", "User");
    }
}