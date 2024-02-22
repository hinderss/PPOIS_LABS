using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
        [TestClass]
    public class ProductInfoTest
    {
        [TestMethod]
        public void TestProductInfo()
        {
            var product1 = new Product("Product 1", 50.50m, "Description 1");

            var expected = $"{product1.Name} - {product1.Price:C} - {product1.Description}";
            Assert.AreEqual(expected, product1.ToString());
        }
    }
}