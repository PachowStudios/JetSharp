using Newtonsoft.Json;

namespace JetSharp.Authentication
{
  public class JetCredentials
  {
    [JsonProperty("user")]
    public string Username { get; }

    [JsonProperty("pass")]
    public string Password { get; }

    public JetCredentials(string username, string password)
    {
      Username = username;
      Password = password;
    }
  }
}