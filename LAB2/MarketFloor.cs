using Minimarket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimarket
{
    public class MarketFloor
    {
        private MarketFloorDBManager databaseManager;

        public MarketFloor(string connectionString, string tableName)
        {
            databaseManager = new MarketFloorDBManager(connectionString, tableName);
        }

        public Product? GetProductByName(string name)
        {
            return databaseManager.Get("Name", name);
        }

        public Product? TakeProductByName(string name)
        {
            return databaseManager.Take("Name", name);
        }

        public Product? TakeProductByBarcode(string barcode)
        {
            return databaseManager.Take("Barcode", barcode);
        }


        public void AddOneProduct(Product product)
        {
            databaseManager.AddItemWithQuantity(product, 1);
        }

        public void AddProductWithQuantity(Product product, int quantity)
        {
            databaseManager.AddItemWithQuantity(product, quantity);
        }
    }
}
