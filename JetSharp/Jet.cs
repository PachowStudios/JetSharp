using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Web;
using JetBrains.Annotations;
using JetSharp.Authentication;
using JetSharp.Products;
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

    private JetCredentials Credentials { get; set; }

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

    public static Task<Jet> CreateAsync([NotNull] string username, [NotNull] string password)
      => CreateAsync(new JetCredentials(username, password));

    public static async Task<Jet> CreateAsync([NotNull] JetCredentials credentials)
    {
      var instance = new Jet();

      await instance.AuthenticateAsync(credentials);

      return instance;
    }

    public Task<JetToken> AuthenticateAsync([NotNull] string username, [NotNull] string password)
      => AuthenticateAsync(new JetCredentials(username, password));

    public async Task<JetToken> AuthenticateAsync([NotNull] JetCredentials credentials)
    {
      Credentials = credentials;

      try
      {
        Authenticator = new JetAuthenticator(
          await ExecuteRequestAsync<JetToken>(
            new JetTokenRequest(credentials)).ConfigureAwait(false));
      }
      catch (HttpException e)
      {
        if (e.ErrorCode == (int)HttpStatusCode.BadRequest)
          throw new AuthenticationException("Invalid username or password");

        throw;
      }

      return Authenticator.Token;
    }

    public async Task<HttpStatusCode> CreateProductAsync([NotNull] Product product)
    {
      var response = await ExecuteAuthenticatedRequestAsync(
        new PutProductRequest(product)).ConfigureAwait(false);

      return response.StatusCode;
    }

    private async Task<TResponse> ExecuteAuthenticatedRequestAsync<TResponse>(IJetRequest request)
    {
      await AuthenticateIfNecessaryAsync().ConfigureAwait(false);

      return await ExecuteRequestAsync<TResponse>(request).ConfigureAwait(false);
    }

    private async Task<IRestResponse> ExecuteAuthenticatedRequestAsync(IJetRequest request)
    {
      await AuthenticateIfNecessaryAsync().ConfigureAwait(false);

      return await ExecuteRequestAsync(request).ConfigureAwait(false);
    }

    private async Task AuthenticateIfNecessaryAsync()
    {
      if (!Authenticator?.IsTokenValid ?? false)
        await AuthenticateAsync(Credentials).ConfigureAwait(false);
    }

    private async Task<TResponse> ExecuteRequestAsync<TResponse>(IJetRequest request)
    {
      var response = await ExecuteRequestAsync(request).ConfigureAwait(false);

      return JsonConvert.DeserializeObject<TResponse>(response.Content);
    }

    private async Task<IRestResponse> ExecuteRequestAsync(IJetRequest request)
    {
      var response = await RestClient.ExecuteTaskAsync(
        new RestRequest(request.Resource, request.Method)
          { JsonSerializer = Serializer }
            .AddJsonBody(request.Body)).ConfigureAwait(false);

      CheckResponseStatusCode(response);

      return response;
    }

    private static void CheckResponseStatusCode(IRestResponse response)
    {
      switch (response.StatusCode)
      {
        case HttpStatusCode.BadRequest:
          throw new HttpException((int)response.StatusCode, response.Content);
        case HttpStatusCode.Unauthorized:
          throw new AuthenticationException();
      }
    }
  }
}