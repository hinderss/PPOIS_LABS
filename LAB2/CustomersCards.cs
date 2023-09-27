using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimarket
{
    public class CustomersCards
    {
        private DiscountCardsDBManager databaseManager;

        public CustomersCards(string connectionString, string tableName)
        {
            databaseManager = new DiscountCardsDBManager(connectionString, tableName);
        }

        public void ActivateCard(DiscountCard card)
        {
            card.Activate(DateTime.Now);
            databaseManager.AddItem(card);
        }

        public DiscountCard? GetCardByNumber(string number)
        {
            return databaseManager.Get("Number", number);
        }
    }
}
