using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class DeleteItemCard : BaseDiscountCardsTest
    {
        private const string TableName = "Cards";

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
        public void TestDeleteItemCard()
        {
            var discountCardDBManager = new DiscountCardsDBManager(ConnectionString, TableName);


            var expected = new DiscountCard("00000000000000000", "Example Example", "000000000000");
            discountCardDBManager.AddItem(expected);

            var actual = discountCardDBManager.Get("Number", expected.Number);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Number, actual.Number);
            Assert.AreEqual(expected.CardHolderName, actual.CardHolderName);
            Assert.AreEqual(expected.CardHolderPhone, actual.CardHolderPhone);

            discountCardDBManager.DeleteItem(expected);

            actual = discountCardDBManager.Get("Number", expected.Number);
            Assert.IsNull(actual);
        }
    }
}