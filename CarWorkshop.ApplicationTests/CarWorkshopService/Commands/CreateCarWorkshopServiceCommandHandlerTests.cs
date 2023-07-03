using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace CarWorkshop.Application.CarWorkshopService.Commands.Tests;

public class CreateCarWorkshopServiceCommandHandlerTests
{
    [Fact()]
    public async Task Handle_WhenUserIsAuthorized_CreatesCarWorkshopService()
    {
        // Arrange
        var carWorkshop = new Domain.Entities.CarWorkshop()
        {
            Id = 1,
            CreatedById = "2"
        };
        var command = new CreateCarWorkshopServiceCommand()
        {
            Cost = "100 PLN",
            Description = "Service description",
            CarWorkshopEncodedName = "workshop1"
        };
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(c => c.GetCurrentUser())
            .Returns(new CurrentUser("2", "test@tests.com", new[] { "User" }));
        var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
        carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName))
            .ReturnsAsync(carWorkshop);
        var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();
        var handler = new CreateCarWorkshopServiceCommandHandler(userContextMock.Object, carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
        carWorkshopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Once);
    }

    [Fact()]
    public async Task Handle_WhenUserIsModerator_CreatesCarWorkshopService_()
    {
        // Arrange
        var carWorkshop = new Domain.Entities.CarWorkshop()
        {
            Id = 1,
            CreatedById = "2"
        };
        var command = new CreateCarWorkshopServiceCommand()
        {
            Cost = "100 PLN",
            Description = "Service description",
            CarWorkshopEncodedName = "workshop1"
        };
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(c => c.GetCurrentUser())
            .Returns(new CurrentUser("3", "test@tests.com", new[] { "Moderator" }));
        var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
        carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName))
            .ReturnsAsync(carWorkshop);
        var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();
        var handler = new CreateCarWorkshopServiceCommandHandler(userContextMock.Object, carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
        carWorkshopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Once);
    }

    [Fact()]
    public async Task Handle_WhenUserIsNotAuthorize_DoesntCarWorkshopService_()
    {
        // Arrange
        var carWorkshop = new Domain.Entities.CarWorkshop()
        {
            Id = 1,
            CreatedById = "2"
        };
        var command = new CreateCarWorkshopServiceCommand()
        {
            Cost = "100 PLN",
            Description = "Service description",
            CarWorkshopEncodedName = "workshop1"
        };
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(c => c.GetCurrentUser())
            .Returns(new CurrentUser("3", "test@tests.com", new[] { "User" }));
        var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
        carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName))
            .ReturnsAsync(carWorkshop);
        var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();
        var handler = new CreateCarWorkshopServiceCommandHandler(userContextMock.Object, carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
        carWorkshopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Never);
    }

    [Fact()]
    public async Task Handle_WhenUserIsNotAutenticated_DoesntCarWorkshopService_()
    {
        // Arrange
        var carWorkshop = new Domain.Entities.CarWorkshop()
        {
            Id = 1,
            CreatedById = "2"
        };
        var command = new CreateCarWorkshopServiceCommand()
        {
            Cost = "100 PLN",
            Description = "Service description",
            CarWorkshopEncodedName = "workshop1"
        };
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(c => c.GetCurrentUser())
            .Returns((CurrentUser?)null);
        var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
        carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName))
            .ReturnsAsync(carWorkshop);
        var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();
        var handler = new CreateCarWorkshopServiceCommandHandler(userContextMock.Object, carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
        carWorkshopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Never);
    }
}