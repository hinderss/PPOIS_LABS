using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class EmptyBarcodeProduct
    {
        [TestMethod]
        public void TestGetProductByName()
        {
            var barcodeP = new BarcodeProduct();

            Assert.AreEqual(barcodeP.Name, "");
            Assert.AreEqual(barcodeP.Price, 0.00M);
            Assert.AreEqual(barcodeP.Description, "");
            Assert.AreEqual(barcodeP.Barcode, "");
        }
    }
}