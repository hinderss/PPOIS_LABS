using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class CreateMarketFloorDBManagerTest: BaseDiscountCardsTest
    {
        [TestMethod]
        public void TestCreateMarketFloorDBManager()
        {
            var manager = new MarketFloorDBManager(ConnectionString, TableName);

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqliteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='{TableName}'", connection);

                using (var reader = command.ExecuteReader())
                {
                    Assert.IsTrue(reader.Read(), "Таблица не была создана.");
                }

            }
        }
    }
}