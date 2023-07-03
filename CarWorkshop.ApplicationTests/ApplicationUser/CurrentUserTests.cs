using FluentAssertions;
using Xunit;

namespace CarWorkshop.Application.ApplicationUser.Tests;

public class CurrentUserTests
{
    [Fact()]
    public void IsInRole_WithMatchinRoleSshouldReturnTrue()
    {
        // Arrange
        CurrentUser currentUser = new CurrentUser("1", "test@tests.com", new List<string> { "Admin", "User"});

        // Act
        bool isInRole = currentUser.IsInRole("Admin");

        // Assert
        isInRole.Should().BeTrue();
    }
    [Fact()]
    public void IsInRole_WithNonMatchinRole_shouldReturnFalse()
    {
        // Arrange
        CurrentUser currentUser = new CurrentUser("1", "test@tests.com", new List<string> { "Admin", "User" });

        // Act
        bool isInRole = currentUser.IsInRole("Moderator");

        // Assert
        isInRole.Should().BeFalse();
    }
    [Fact()]
    public void IsInRole_WithNonMatchinCaseRole_shouldReturnFalse()
    {
        // Arrange
        CurrentUser currentUser = new CurrentUser("1", "test@tests.com", new List<string> { "Admin", "User" });

        // Act
        bool isInRole = currentUser.IsInRole("admin");

        // Assert
        isInRole.Should().BeFalse();
    }
}