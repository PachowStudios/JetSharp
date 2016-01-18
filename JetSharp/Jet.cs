using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Web;
using JetBrains.Annotations;
using JetSharp.Authentication;
using JetSharp.Serialization;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace JetSharp
{
  public class Jet
  {
    private const string ApiUrl = "https://merchant-api.jet.com/api";

    private IRestClient RestClient { get; }
    private ISerializer Serializer { get; }

    private string Username { get; set; }
    private string Password { get; set; }

    private JetAuthenticator Authenticator
    {
      get { return (JetAuthenticator)RestClient.Authenticator; }
      set { RestClient.Authenticator = value; }
    }

    [CanBeNull]
    public JetToken AuthenticationToken => Authenticator?.Token;

    public Jet()
    {
      RestClient = new RestClient(ApiUrl);

      Serializer = new JsonDotNetSerializer(new JsonSerializer
      {
        ContractResolver = new PrivatePropertyContractResolver(),
        MissingMemberHandling = MissingMemberHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        NullValueHandling = NullValueHandling.Include
      });
    }

    public static async Task<Jet> CreateAsync([NotNull] string username, [NotNull] string password)
    {
      var instance = new Jet();

      await instance.AuthenticateAsync(username, password);

      return instance;
    }

    public async Task<JetToken> AuthenticateAsync([NotNull] string username, [NotNull] string password)
    {
      Username = username;
      Password = password;

      try
      {
        Authenticator = new JetAuthenticator(
          await ExecuteRequestAsync<JetTokenRequest, JetToken>(
            new JetTokenRequest(username, password)).ConfigureAwait(false));
      }
      catch (HttpException e)
      {
        if (e.ErrorCode == (int)HttpStatusCode.BadRequest)
          throw new AuthenticationException("Invalid username or password");

        throw;
      }

      return Authenticator.Token;
    }

    private async Task<TResponse> ExecuteAuthenticatedRequestAsync<TRequest, TResponse>(TRequest request)
      where TRequest : JetRequest
    {
      if (!Authenticator?.IsTokenValid ?? false)
        await AuthenticateAsync(Username, Password).ConfigureAwait(false);

      return await ExecuteRequestAsync<TRequest, TResponse>(request).ConfigureAwait(false);
    }

    private async Task<TResponse> ExecuteRequestAsync<TRequest, TResponse>(TRequest request)
      where TRequest : JetRequest
    {
      var response = await RestClient.ExecuteTaskAsync(
        new RestRequest(request.Resource, request.Method)
        { JsonSerializer = Serializer }
          .AddJsonBody(request)).ConfigureAwait(false);

      CheckResponseStatusCode(response);

      return JsonConvert.DeserializeObject<TResponse>(response.Content);
    }

    private static void CheckResponseStatusCode(IRestResponse response)
    {
      switch (response.StatusCode)
      {
        case HttpStatusCode.BadRequest:
          throw new HttpException((int)response.StatusCode, response.StatusDescription);
        case HttpStatusCode.Unauthorized:
          throw new AuthenticationException();
      }
    }
  }
}