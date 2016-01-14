using System;
using RestSharp;

namespace JetSharp
{
  [AttributeUsage(AttributeTargets.Class)]
  internal class JetResourceAttribute : Attribute
  {
    public Method Method { get; }
    public string Resource { get; }

    public JetResourceAttribute(Method method, string resource)
    {
      Method = method;
      Resource = resource;
    }
  }
}