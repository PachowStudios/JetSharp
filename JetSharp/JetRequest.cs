using System;
using RestSharp;

namespace JetSharp
{
  internal abstract class JetRequest
  {
    private Lazy<JetResourceAttribute> ResourceInfo { get; }

    public Method Method => ResourceInfo.Value.Method;
    public string Resource => ResourceInfo.Value.Resource;

    protected JetRequest()
    {
      ResourceInfo = new Lazy<JetResourceAttribute>(this.GetJetResource);
    }
  }
}