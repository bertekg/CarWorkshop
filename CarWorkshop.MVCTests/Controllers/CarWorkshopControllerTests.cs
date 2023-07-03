using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Moq;
using System.Net;
using Xunit;

namespace CarWorkshop.MVC.Controllers.Tests;

public class CarWorkshopControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CarWorkshopControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    [Fact()]
    public async Task Index_ForExisitngWorkshops_ReturnViewWithExpectedData()
    {
        // Arrange
        var carWorkshop = new List<CarWorkshopDto>
        {
            new CarWorkshopDto
            {
                Name = "Workshop 1"
            },
            new CarWorkshopDto
            {
                Name = "Workshop 2"
            },
            new CarWorkshopDto
            {
                Name = "Workshop 3"
            }
        };
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCarWorkshopsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(carWorkshop);
        var clinet = _factory
            .WithWebHostBuilder(builder => 
                builder.ConfigureTestServices(services => services.AddScoped(_ => mediatorMock.Object)))
            .CreateClient();

        // Act
        var response = await clinet.GetAsync("/CarWorkshop/Index");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("<h1>Car Workshops</h1>")
                .And.Contain("Workshop 1")
                .And.Contain("Workshop 2")
                .And.Contain("Workshop 3");
    }
    [Fact()]
    public async Task Index_ForNoCarWorkshpsExisit_ReturnEmptyView()
    {
        // Arrange
        var carWorkshop = new List<CarWorkshopDto>();
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCarWorkshopsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(carWorkshop);
        var clinet = _factory
            .WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services => services.AddScoped(_ => mediatorMock.Object)))
            .CreateClient();

        // Act
        var response = await clinet.GetAsync("/CarWorkshop/Index");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotContain("div class=\"card m-3\"");
    }
}