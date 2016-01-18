using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace JetSharp.Tests
{
  [TestFixture]
  public class JetTests : BaseJetTest
  {
    [Test]
    public async Task TestAuthenticateAsync()
    {
      var jet = new Jet();
      var token = await jet.AuthenticateAsync(Credentials);

      token.Should().NotBeNull("because the login completed");
      token.IsExpired.Should().BeFalse("because the token is valid");
    }
  }
}
