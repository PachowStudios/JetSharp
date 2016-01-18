using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using JetSharp.Products;
using NUnit.Framework;

namespace JetSharp.Tests
{
  [TestFixture]
  public class ProductTests : BaseJetTest
  {
    private const int TestSKU = 12345;
    private const long TestUPC = 123456789012;

    [Test]
    public async Task TestCreateProductAsync()
    {
      var jet = await Jet.CreateAsync(Credentials);

      var statusCode = await jet.CreateProductAsync(
        new Product
        {
          SKU = TestSKU,
          Manufacturer = "China",
          Title = "Test product",
          Description = "A very nice product",
          StandardProductCodes = new List<StandardProductCode>
          {
            new StandardProductCode(TestUPC, StandardProductCodeType.UPC)
          },
          MultipackQuantity = 1,
          MapImplementation = MapImplementation.NeverApplied
        });

      statusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Test]
    public async Task TestSetProductPriceAsync()
    {
      var jet = await Jet.CreateAsync(Credentials);

      var statusCode = await jet.SetProductPrice(
        new ProductPrice(TestSKU, 69.99m));

      statusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Test]
    public async Task TestSetProductInventoryAsync()
    {
      var jet = await Jet.CreateAsync(Credentials);

      var statusCode = await jet.SetProductInventory(
        new ProductInventory(
          TestSKU,
          new FulfillmentNode("fa3ff0cab54f4ac88bff1f5342f08380", 5)));

      statusCode.Should().Be(HttpStatusCode.NoContent);
    }
  }
}