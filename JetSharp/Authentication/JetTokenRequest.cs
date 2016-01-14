using Newtonsoft.Json;
using RestSharp;

namespace JetSharp.Authentication
{
  [JetResource(Method.POST, "token")]
  internal class JetTokenRequest : JetRequest
  {
    [JsonProperty("user")]
    public string Username { get; }

    [JsonProperty("pass")]
    public string Password { get; }

    public JetTokenRequest(string username, string password)
    {
      Username = username;
      Password = password;
    }
  }
}