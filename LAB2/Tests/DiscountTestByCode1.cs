using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class DiscountTestByCode1: BaseDiscountCardsTest
    {
        private PaymentData payment = new PaymentData(
                "ООО \"Example\"",
                "BY30UNBS31581568200000000784",
                "UNBSBY2X",
                "197845668",
                "Оплата в минимаркете",
                "OTHR",
                "208956");

        private const string TableNameCards = "Cards";

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

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableNameCards}", connection);
                command.ExecuteNonQuery();
            }


        }

        [TestMethod]
        public void TestCashier()
        {
            var discountCardsManager = new DiscountCardsDBManager(ConnectionString, TableNameCards);
            var card = new DiscountCard("00000000000000000", "Example Example", "000000000000", DateTime.Parse("2023-09-25 22:10:07.6113453"));
            discountCardsManager.AddItem(card);

            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");


            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            Product product1 = new Product("Product 1", 100.0m, "Description 1");
            marketFloor.AddProductWithQuantity(product1, 2);

            cashier.AddToReceiptByName("Product 1");

            cashier.ApplyDiscountCard("00000000000000000");

            Assert.AreEqual(95.0M, cashier.TotalOrder);
        }
    }
}