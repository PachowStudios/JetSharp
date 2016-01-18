using RestSharp;

namespace JetSharp.Authentication
{
  [JetResource(Method.POST, "token")]
  internal class JetTokenRequest : JetRequest<JetCredentials>
  {
    public JetTokenRequest(JetCredentials body)
      : base(body) { }
  }
}