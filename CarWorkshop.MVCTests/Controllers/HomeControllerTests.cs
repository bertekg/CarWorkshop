using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace CarWorkshop.MVC.Controllers.Tests;

public class HomeControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public HomeControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    [Fact()]
    public async Task About_ReturnsCiweWithRenderModel()
    {
        // Arrange
        var clinet = _factory.CreateClient();

        // Act
        var response = await clinet.GetAsync("/Home/About");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("<h1>CarWorkshop application</h1>")
                .And.Contain("<div class=\"alert alert-info\">Some awesome description</div>")
                .And.Contain("<li>car</li>")
                .And.Contain("<li>app</li>")
                .And.Contain("<li>free</li>");
    }
}