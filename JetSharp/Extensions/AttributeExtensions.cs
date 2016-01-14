using System.Reflection;

namespace JetSharp
{
  internal static class AttributeExtensions
  {
    internal static JetResourceAttribute GetJetResource<T>(this T @object)
      where T : class
      => @object.GetType().GetCustomAttribute<JetResourceAttribute>();
  }
}