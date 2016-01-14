using RestSharp;
using RestSharp.Authenticators;

namespace JetSharp.Authentication
{
  internal class JetAuthenticator : IAuthenticator
  {
    public JetToken Token { get; }

    public bool IsTokenValid => !Token?.IsExpired ?? false;

    public JetAuthenticator(JetToken token)
    {
      Token = token;
    }

    public void Authenticate(IRestClient client, IRestRequest request)
    {
      if (IsTokenValid)
        request.AddHeader("Authorization", Token.ToString());
    }
  }
}