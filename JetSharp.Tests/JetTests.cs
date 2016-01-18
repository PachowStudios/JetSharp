using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace JetSharp.Tests
{
  [TestFixture]
  public class JetTests
  {
    private string Username { get; set; }
    private string Password { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      Username = Environment.GetEnvironmentVariable("JET_USER", EnvironmentVariableTarget.User);
      Password = Environment.GetEnvironmentVariable("JET_PASS", EnvironmentVariableTarget.User);
    }

    [Test]
    public async Task TestAuthenticateAsync()
    {
      var jet = new Jet();
      var token = await jet.AuthenticateAsync(Username, Password);

      token.Should().NotBeNull("because the login completed");
      token.IsExpired.Should().BeFalse("because the token is valid");
    }
  }
}
