using Microsoft.Data.Sqlite;
using Minimarket;

namespace MinimarketTests
{
    [TestClass]
    public class DiscountCardActivateTest : BaseDiscountCardsTest
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
            var card = new DiscountCard("00000000000000000", "Example Example", "000000000000");
            discountCardsManager.AddItem(card);

            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);
            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");


            Cashier cashier = new Cashier(marketFloor, payment, customersCards);

            cashier.ActivateDiscountCard(card);

            var actualCard = customersCards.GetCardByNumber("00000000000000000");
            Assert.AreEqual(card.Number, actualCard.Number);
            Assert.AreEqual(card.CardHolderName, actualCard.CardHolderName);
            Assert.AreEqual(card.CardHolderPhone, actualCard.CardHolderPhone);
        }
    }
}