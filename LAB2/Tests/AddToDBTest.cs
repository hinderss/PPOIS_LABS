using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class AddToDB : BaseDiscountCardsTest
    {
        private PaymentData payment = new PaymentData(
                "ООО \"Example\"",
                "BY30UNBS31581568200000000784",
                "UNBSBY2X",
                "197845668",
                "Оплата в минимаркете",
                "OTHR",
                "208956");


        [TestInitialize]
        public void Initialize()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestAddToDB()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");

            var cashier = new Cashier(marketFloor, payment, customersCards);
            var expected = new BarcodeProduct("Barcode Product 1", 10.0m, "Description 1", "487878787887875487847");
            marketFloor.AddProductWithQuantity(expected, 10);

            string selectQuery = $"SELECT * FROM {TableName} WHERE Barcode = @Value";

            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Value", expected.Barcode);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        Assert.IsTrue(reader.Read());

                    }
                }
            }
        }
    }

}