using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class DeleteTableCardsTest: BaseDiscountCardsTest
    {
        private new const string TableName = "DiscountCards";

        [TestMethod]
        public void DeleteTableCards()
        {
            var manager = new DiscountCardsDBManager(ConnectionString, TableName);
            //manager.DeleteTable();

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