using System;
using RestSharp;

namespace JetSharp
{
  internal interface IJetRequest
  {
    object Body { get; }
    Method Method { get; }
    string Resource { get; }
  }

  internal abstract class JetRequest<T> : IJetRequest
    where T : class
  {
    private Lazy<JetResourceAttribute> ResourceInfo { get; }
    private Lazy<string> BuiltResourcePath { get; }

    protected virtual object[] ResourceParameters { get; } = { };

    public T Body { get; }

    object IJetRequest.Body => Body;
    Method IJetRequest.Method => ResourceInfo.Value.Method;
    string IJetRequest.Resource => BuiltResourcePath.Value;

    protected JetRequest(T body)
    {
      Body = body;
      ResourceInfo = new Lazy<JetResourceAttribute>(this.GetJetResource);
      BuiltResourcePath = new Lazy<string>(BuildResourcePath);
    }

    private string BuildResourcePath()
      => string.Format(ResourceInfo.Value.Resource, ResourceParameters);
  }
}