using RestSharp;

namespace JetSharp.Authentication
{
  [JetResource(Method.POST, "token")]
  internal class JetTokenRequest : JetRequest<JetCredentials>
  {
    public override JetCredentials Body { get; }

    public JetTokenRequest(JetCredentials body)
    {
      Body = body;
    }
  }
}