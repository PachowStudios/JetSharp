using Newtonsoft.Json;

namespace JetSharp.Products
{
  public enum StandardProductCodeType
  {
    UPC,
    ASIN
  }

  public class StandardProductCode
  {
    [JsonProperty("standard_product_code")]
    public string Code { get; set; }

    [JsonIgnore]
    public StandardProductCodeType CodeType { get; set; }

    [JsonProperty("standard_product_code_type")]
    private string CodeTypeString
    {
      get { return CodeType.ToString(); }
      set { CodeType = value.ToEnum<StandardProductCodeType>(); }
    }

    public StandardProductCode(long code, StandardProductCodeType codeType)
    {
      Code = code.ToString();
      CodeType = codeType;
    }
  }
}