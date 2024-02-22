using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class EmptyProductTest
    {
        [TestMethod]
        public void TestEmptyProduct()
        {
            var emptyProduct = new Product();

            Assert.AreEqual("", emptyProduct.Name);
            Assert.AreEqual(0.00M, emptyProduct.Price);
            Assert.AreEqual("", emptyProduct.Description);
        }
    }
}