using RestSharp;

namespace JetSharp.Products
{
  [JetResource(Method.PUT, "merchant-skus/{0}")]
  internal class PutProductRequest : JetRequest<Product>
  {
    protected override object[] ResourceParameters => new object[]
    {
      Body.SKU.ToString()
    };

    public PutProductRequest(Product body)
      : base(body) { }
  }
}