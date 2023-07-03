using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace CarWorkshop.Application.Mappings.Tests;

public class CarWorkshopMappingProfileTests
{
    [Fact()]
    public void MappingProfile_ShouldMapCarWorkshopDtoToCarWorkshop()
    {
        // Arrange
        var userContectMock = new Mock<IUserContext>();
        userContectMock.Setup(c => c.GetCurrentUser())
            .Returns(new CurrentUser("1", "test@tests.com", new[] { "Moderator" }));
        var configuration = new MapperConfiguration(cfg => 
        cfg.AddProfile(new CarWorkshopMappingProfile(userContectMock.Object)));
        var mapper = configuration.CreateMapper();
        var dto = new CarWorkshopDto
        {
            City = "Some City",
            PhoneNumber = "123456789",
            PostalCode = "98 - 765",
            Street = "Some Street"
        };

        // Act
        var result = mapper.Map<Domain.Entities.CarWorkshop>(dto);

        // Assert
        result.Should().NotBeNull();
        result.ContactDetails.Should().NotBeNull();
        result.ContactDetails.City.Should().Be(dto.City);
        result.ContactDetails.PhoneNumber.Should().Be(dto.PhoneNumber);
        result.ContactDetails.PostalCode.Should().Be(dto.PostalCode);
        result.ContactDetails.Street.Should().Be(dto.Street);
    }

    [Fact()]
    public void MappingProfile_ForSameUser_ShouldMapCarWorhopToCarWorkshopDto()
    {
        // Arrange
        var userContectMock = new Mock<IUserContext>();
        userContectMock.Setup(c => c.GetCurrentUser())
            .Returns(new CurrentUser("1", "test@tests.com", new[] { "User" }));
        var configuration = new MapperConfiguration(cfg =>
        cfg.AddProfile(new CarWorkshopMappingProfile(userContectMock.Object)));
        var mapper = configuration.CreateMapper();
        var carWorkshop = new Domain.Entities.CarWorkshop
        {
            Id = 1,
            CreatedById = "1",
            ContactDetails = new CarWorkshopContactDetails
            {
                City = "Some City",
                PhoneNumber = "123456789",
                PostalCode = "98 - 765",
                Street = "Some Street"
            }
        };

        // Act
        var result = mapper.Map<CarWorkshopDto>(carWorkshop);

        //Assert
        result.Should().NotBeNull();
        result.IsEditable.Should().BeTrue();
        result.City.Should().Be(carWorkshop.ContactDetails.City);
        result.PhoneNumber.Should().Be(carWorkshop.ContactDetails.PhoneNumber);
        result.PostalCode.Should().Be(carWorkshop.ContactDetails.PostalCode);
        result.Street.Should().Be(carWorkshop.ContactDetails.Street);
    }
    [Fact()]
    public void MappingProfile_ForModerator_ShouldMapCarWorhopToCarWorkshopDto()
    {
        // Arrange
        var userContectMock = new Mock<IUserContext>();
        userContectMock.Setup(c => c.GetCurrentUser())
            .Returns(new CurrentUser("2", "test@tests.com", new[] { "Moderator" }));
        var configuration = new MapperConfiguration(cfg =>
        cfg.AddProfile(new CarWorkshopMappingProfile(userContectMock.Object)));
        var mapper = configuration.CreateMapper();
        var carWorkshop = new Domain.Entities.CarWorkshop
        {
            Id = 1,
            CreatedById = "1",
            ContactDetails = new CarWorkshopContactDetails
            {
                City = "Some City",
                PhoneNumber = "123456789",
                PostalCode = "98 - 765",
                Street = "Some Street"
            }
        };

        // Act
        var result = mapper.Map<CarWorkshopDto>(carWorkshop);

        //Assert
        result.Should().NotBeNull();
        result.IsEditable.Should().BeTrue();
        result.City.Should().Be(carWorkshop.ContactDetails.City);
        result.PhoneNumber.Should().Be(carWorkshop.ContactDetails.PhoneNumber);
        result.PostalCode.Should().Be(carWorkshop.ContactDetails.PostalCode);
        result.Street.Should().Be(carWorkshop.ContactDetails.Street);
    }
    [Fact()]
    public void MappingProfile_ForNonSameUser_ShouldMapCarWorhopToCarWorkshopDto()
    {
        // Arrange
        var userContectMock = new Mock<IUserContext>();
        userContectMock.Setup(c => c.GetCurrentUser())
            .Returns(new CurrentUser("2", "test@tests.com", new[] { "User" }));
        var configuration = new MapperConfiguration(cfg =>
        cfg.AddProfile(new CarWorkshopMappingProfile(userContectMock.Object)));
        var mapper = configuration.CreateMapper();
        var carWorkshop = new Domain.Entities.CarWorkshop
        {
            Id = 1,
            CreatedById = "1",
            ContactDetails = new CarWorkshopContactDetails
            {
                City = "Some City",
                PhoneNumber = "123456789",
                PostalCode = "98 - 765",
                Street = "Some Street"
            }
        };

        // Act
        var result = mapper.Map<CarWorkshopDto>(carWorkshop);

        //Assert
        result.Should().NotBeNull();
        result.IsEditable.Should().BeFalse();
        result.City.Should().Be(carWorkshop.ContactDetails.City);
        result.PhoneNumber.Should().Be(carWorkshop.ContactDetails.PhoneNumber);
        result.PostalCode.Should().Be(carWorkshop.ContactDetails.PostalCode);
        result.Street.Should().Be(carWorkshop.ContactDetails.Street);
    }
}