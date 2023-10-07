using RideSharing.Common.ValueObjects;

namespace RideSharing.Common.UnitTests.ValueObjects;

public class EmailTests
{
    private const string ValidEmail = "abc@gmail.com";
    private const string InvalidEmail = "abc@gmail";

    [SetUp]
    public void SetUp()
    {
        
    }

    [Test]
    public void IsValid_WithValidEmail_ReturnsTrue()
    {
        var result = Email.IsValid(ValidEmail);
        Assert.IsTrue(result);
    }

    [Test]
    public void IsValid_WithInvalidEmail_ReturnsFalse()
    {
        var result = Email.IsValid(InvalidEmail);
        Assert.IsFalse(result);
    }

    [Test]
    public void Constructor_WithValidEmail_ThrowsException()
    {
        FluentActions.Invoking(() => new Email(ValidEmail))
            .Should().NotThrow<Exception>();
    }

    [Test]
    public void Constructor_WithInvalidEmail_ThrowsException()
    {
        FluentActions.Invoking(() => new Email(ValidEmail))
            .Should().Throw<Exception>();
    }

}
