using RestSharp;

namespace JetSharp.Products
{
  [JetResource(Method.PUT, "merchant-skus/{0}")]
  internal class CreateProductRequest : JetRequest<Product>
  {
    public override Product Body { get; }

    protected override object[] ResourceParameters => new object[]
    {
      Body.SKU.ToString()
    };

    public CreateProductRequest(Product body)
    {
      Body = body;
    }
  }

  [JetResource(Method.PUT, "merchant-skus/{0}/price")]
  internal class SetProductPriceRequest : JetRequest<ProductPrice>
  {
    public override ProductPrice Body { get; }

    protected override object[] ResourceParameters => new object[]
    {
      Body.SKU.ToString()
    };

    public SetProductPriceRequest(ProductPrice body)
    {
      Body = body;
    }
  }

  [JetResource(Method.PUT, "merchant-skus/{0}/inventory")]
  internal class SetProductInventoryRequest : JetRequest<ProductInventory>
  {
    public override ProductInventory Body { get; }

    protected override object[] ResourceParameters => new object[]
    {
      Body.SKU.ToString()
    };

    public SetProductInventoryRequest(ProductInventory body)
    {
      Body = body;
    }
  }
}