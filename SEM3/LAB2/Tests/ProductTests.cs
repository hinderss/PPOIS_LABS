using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void Print_Product_PrintsCorrectly()
        {
            // Arrange
            string expectedOutput = "Название: TestProduct\r\nЦена: 10,00 Br\r\nОписание: TestDescription"; // Замените значения на свои
            Product product = new Product("TestProduct", 10.00M, "TestDescription");

            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                product.Print();
                string printedOutput = sw.ToString().Trim();

                // Assert
                StringAssert.Contains(printedOutput, expectedOutput);
            }
        }
    }
}