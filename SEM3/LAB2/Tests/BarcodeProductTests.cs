using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class BarcodeProductTests
    {
        [TestMethod]
        public void Print_BarcodeProduct_PrintsCorrectly()
        {
            // Arrange
            string expectedOutput = "Название: TestProduct\r\nЦена: 10,00 Br\r\nОписание: TestDescription\r\nКод: TestBarcode"; // Замените значения на свои
            BarcodeProduct barcodeProduct = new BarcodeProduct("TestProduct", 10.00M, "TestDescription", "TestBarcode");

            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                barcodeProduct.Print();
                string printedOutput = sw.ToString().Trim();

                // Assert
                StringAssert.Contains(printedOutput, expectedOutput);
            }
        }
    }
}