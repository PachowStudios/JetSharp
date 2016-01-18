using Newtonsoft.Json;

namespace JetSharp.Products
{
  public class FulfillmentNode
  {
    [JsonProperty("fulfillment_node_id")]
    public string NodeId { get; set; }

    [JsonProperty("quantity")]
    public int Quantity { get; set; }

    public FulfillmentNode(string nodeId, int quantity)
    {
      NodeId = nodeId;
      Quantity = quantity;
    }
  }
}