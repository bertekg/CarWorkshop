using FluentAssertions;
using Xunit;

namespace CarWorkshop.Domain.Entities.Tests;

public class CarWorkshopTests
{
    [Theory]
    [InlineData("Test Workshop", "test-workshop")]
    [InlineData("Warsztat", "warsztat")]
    [InlineData("Super Warsztat Pana Marka 2", "super-warsztat-pana-marka-2")]
    public void EncodeName_ForSetName_ShouldSetEncodedNam(string name, string encodedNameExpected)
    {
        // Arrange
        var carWorkshop = new CarWorkshop
        {
            Name = name
        };

        // Act
        carWorkshop.EncodeName();

        // Assert
        carWorkshop.EncodedName.Should().Be(encodedNameExpected);
    }
    [Fact()]
    public void EncodeName_ForNullName_ShouldThrowException()
    {
        // Arrange
        var carWorkshop = new CarWorkshop();

        // Act
        Action action = carWorkshop.EncodeName;

        // Assert
        action.Invoking(a => a.Invoke())
            .Should().Throw<NullReferenceException>();
    }
}