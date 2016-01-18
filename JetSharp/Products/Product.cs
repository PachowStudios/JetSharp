using System.Collections.Generic;
using Newtonsoft.Json;

namespace JetSharp.Products
{
  public class Product
  {
    [JsonIgnore]
    public int SKU { get; set; }

    [JsonProperty("manufacturer")]
    public string Manufacturer { get; set; }

    [JsonProperty("product_title")]
    public string Title { get; set; }

    [JsonProperty("product_description")]
    public string Description { get; set; }

    [JsonProperty("standard_product_codes")]
    public List<StandardProductCode> StandardProductCodes { get; set; }

    [JsonProperty("multipack_quantity")]
    public int MultipackQuantity { get; set; } = 1;

    [JsonProperty("fulfillment_time")]
    public int FulfillmentTime { get; set; }

    [JsonIgnore]
    public MapImplementation MapImplementation { get; set; }

    [JsonProperty("map_implementation")]
    public string MapImplementationString
    {
      get { return ((int)MapImplementation).ToString(); }
      set { MapImplementation = (MapImplementation)int.Parse(value); }
    }
  }
}