using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class CashierTestByCode1 : BaseDiscountCardsTest
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
        public void TestCashier()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");

            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            Product product1 = new BarcodeProduct("Barcode Product 1", 454.48m, "Description 1", "487878787887879878");
            marketFloor.AddProductWithQuantity(product1, 2);

            cashier.AddToReceiptByCode("487878787887879878");

            Assert.AreEqual(454.48M, cashier.TotalOrder);
        }
    }
}