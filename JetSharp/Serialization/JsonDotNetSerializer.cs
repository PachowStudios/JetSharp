using System.IO;
using Newtonsoft.Json;
using RestSharp.Serializers;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace JetSharp.Serialization
{
  public class JsonDotNetSerializer : ISerializer
  {
    private readonly JsonSerializer serializer;

    public string ContentType { get; set; } = "application/json";
    public string DateFormat { get; set; }
    public string RootElement { get; set; }
    public string Namespace { get; set; }

    public JsonDotNetSerializer()
      :this(new JsonSerializer()) { }

    public JsonDotNetSerializer(JsonSerializer serializer)
    {
      this.serializer = serializer;
    }

    public string Serialize(object obj)
    {
      using (var stringWriter = new StringWriter())
      using (var jsonTextWriter = new JsonTextWriter(stringWriter))
      {
        jsonTextWriter.Formatting = Formatting.Indented;
        jsonTextWriter.QuoteChar = '"';

        this.serializer.Serialize(jsonTextWriter, obj);

        return stringWriter.ToString();
      }
    }
  }
}