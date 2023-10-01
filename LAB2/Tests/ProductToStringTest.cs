using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class ProductToStringTest
    {
        [TestMethod]
        public void TestProductInfo()
        {
            var product1 = new Product("Product 1", 50.50m, "Description 1");

            var expected = $"{product1.Name} - {product1.Price:C} - {product1.Description}";
            Assert.AreEqual(expected, product1.ToString());
        }
        
        [TestMethod]
        public void TestBarcodeProductInfo()
        {
            var product1 = new BarcodeProduct("Product 1", 50.50m, "Description 1", "48788787494646469456");

            var expected = $"{product1.Name} - {product1.Barcode} - {product1.Price:C} - {product1.Description}";
            Assert.AreEqual(expected, product1.ToString());
        }
    }
}