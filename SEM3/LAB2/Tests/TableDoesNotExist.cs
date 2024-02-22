using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class TableDoesNotExist: BaseDiscountCardsTest{

        [TestMethod]
        public void TestTableDoesNotExist()
        {
            var manager = new MarketFloorDBManager(ConnectionString, TableName);

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='Name'", connection);

                using (var reader = command.ExecuteReader())
                {
                    Assert.IsFalse(reader.Read(), "Таблица не была удалена.");
                }
            }
        }
    }
}