using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class DeleteItemProducts : BaseDiscountCardsTest
    {

        [TestInitialize]
        public void Initialize()
        {
            // Удалить все данные из таблицы перед выполнением теста
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestDeleteItemProducts()
        {
            var marketFloorDBManager = new MarketFloorDBManager(ConnectionString, TableName);


            var expected = new Product("Product 1", 100.0m, "Description 1");
            marketFloorDBManager.AddItem(expected);

            var actual = marketFloorDBManager.Get("Name", "Product 1");

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.Description, actual.Description);

            marketFloorDBManager.DeleteItem(expected);

            actual = marketFloorDBManager.Get("Name", "Product 1");
            Assert.IsNull(actual);
        }
    }
}