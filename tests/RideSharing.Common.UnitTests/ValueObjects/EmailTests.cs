using RideSharing.Common.ValueObjects;

namespace RideSharing.Common.UnitTests.ValueObjects;

public class EmailTests
{
    private const string ValidEmail = "abc@gmail.com";
    private const string InvalidEmail = "abc@gmail";

    [Fact]
    public void IsValid_WithValidEmail_ReturnsTrue()
    {
        var result = Email.IsValid(ValidEmail);
        Assert.True(result);
    }

    [Fact]
    public void IsValid_WithInvalidEmail_ReturnsFalse()
    {
        var result = Email.IsValid(InvalidEmail);
        Assert.False(result);
    }

    [Fact]
    public void Constructor_WithValidEmail_ThrowsException()
    {
        FluentActions.Invoking(() => new Email(ValidEmail))
            .Should().NotThrow<Exception>();
    }

    [Fact]
    public void Constructor_WithInvalidEmail_ThrowsException()
    {
        FluentActions.Invoking(() => new Email(InvalidEmail))
            .Should().Throw<Exception>();
    }

}
