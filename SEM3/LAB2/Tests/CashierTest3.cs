using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class CashierTest3 : BaseDiscountCardsTest
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
            // Удалить все данные из таблицы перед выполнением теста
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestNoMoreProduct()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");

            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            Product product1 = new Product("Product 1", 10.0m, "Description 1");
            marketFloor.AddProductWithQuantity(product1, 0);


            Assert.ThrowsException<Exception>(() => cashier.AddToReceiptByName("Product 1"));
            Assert.AreEqual(0.0M, cashier.TotalOrder);
        }
    }
}