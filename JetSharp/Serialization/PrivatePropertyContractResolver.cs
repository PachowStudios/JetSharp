using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JetSharp.Serialization
{
  public class PrivatePropertyContractResolver : DefaultContractResolver
  {
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
      var jsonProperty = base.CreateProperty(member, memberSerialization);

      var property = member as PropertyInfo;

      if (property == null)
        return jsonProperty;

      jsonProperty.Readable = property.GetMethod != null;
      jsonProperty.Writable = property.SetMethod != null;

      return jsonProperty;
    }
  }
}