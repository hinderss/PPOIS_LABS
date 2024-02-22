using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
        [TestClass]
    public class ProductUpdatePriceTest
    {
        [TestMethod]
        public void TestProductUpdatePrice()
        {
            var emptyProduct = new Product();

            emptyProduct.Price = 17.58M;

            Assert.AreEqual(17.58M, emptyProduct.Price);
        }
    }
}