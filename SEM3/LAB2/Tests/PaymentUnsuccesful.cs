using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class Payment : BaseDiscountCardsTest
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
        public void PaymentUnsuccesfulTest1()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");

            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            var product1 = new Product("Product 1", 11.89m, "Description 1");
            var product2 = new BarcodeProduct("Product 2", 20.25m, "Description 2", "545458458455746676746");
            marketFloor.AddProductWithQuantity(product1, 10);
            marketFloor.AddProductWithQuantity(product2, 10);

            cashier.AddToReceiptByName(product1.Name);
            cashier.AddToReceiptByCode(product2.Barcode);

            Assert.AreEqual(11.89m + 20.25m, cashier.TotalOrder);

            Assert.ThrowsException<ArgumentException>(() => cashier.Transaction("11111111aaa1111", "000"));
        }
    }
}