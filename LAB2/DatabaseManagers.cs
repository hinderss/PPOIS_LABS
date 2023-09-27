using Microsoft.Data.Sqlite;

namespace Minimarket
{
    public class DiscountCardsManager : IDatabaseManager<DiscountCard>
    {
        private string _connectionString;
        private string _tableName;

        public DiscountCardsManager(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            CreateNewTable(tableName);
        }

        public void CreateNewTable(string tableName)
        {
            _tableName = tableName;
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string createTableQuery = $"CREATE TABLE IF NOT EXISTS {_tableName} (" +
                         "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                         "Number TEXT NOT NULL," +
                         "CardHolderName TEXT NOT NULL," +
                         "CardHolderPhone TEXT NOT NULL," +
                         "CreationDate DATE NOT NULL)";

                using (SqliteCommand command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTable()
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string deleteTableQuery = $"DROP TABLE IF EXISTS {_tableName}";

                using (SqliteCommand command = new SqliteCommand(deleteTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddItem(DiscountCard card)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = $"INSERT INTO {_tableName} (Number, CardHolderName, CardHolderPhone, CreationDate) " +
                                    "VALUES (@Number, @CardHolderName, @CardHolderPhone, @CreationDate)";

                using (SqliteCommand command = new SqliteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Number", card.Number);
                    command.Parameters.AddWithValue("@CardHolderName", card.CardHolderName);
                    command.Parameters.AddWithValue("@CardHolderPhone", card.CardHolderPhone);
                    command.Parameters.AddWithValue("@CreationDate", card.CreationDate);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteItem(DiscountCard card)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string deleteQuery = $"DELETE FROM {_tableName} WHERE Number = @Number";

                using (SqliteCommand command = new SqliteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Number", card.Number);

                    command.ExecuteNonQuery();
                }
            }
        }

        public DiscountCard? Get(string field, string value)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = $"SELECT * FROM {_tableName} WHERE {field} = @Value";

                using (SqliteCommand command = new SqliteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Value", value);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string number = reader["Number"].ToString();
                            string cardHolderName = reader["CardHolderName"].ToString();
                            string cardHolderPhone = reader["CardHolderPhone"].ToString();
                            DateTime dateTime = DateTime.Parse(reader["CreationDate"].ToString());

                            return new DiscountCard(number, cardHolderName, cardHolderPhone, dateTime);
                        }
                    }
                }
            }

            return null;
        }
    }

    public class MarketFloorDBManager : IDatabaseManager<Product>
    {
        private string _connectionString;
        private string _tableName;

        public MarketFloorDBManager(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            CreateNewTable(tableName);
        }

        public void CreateNewTable(string tableName)
        {
            _tableName = tableName;
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string createTableQuery = $"CREATE TABLE IF NOT EXISTS {tableName} (" +
                         "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                         "Name TEXT NOT NULL," +
                         "Price DECIMAL NOT NULL," +
                         "Description TEXT," +
                         "Barcode TEXT," +
                         "Quantity INTEGER NOT NULL DEFAULT 0)";

                using (SqliteCommand command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTable()
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string deleteTableQuery = $"DROP TABLE IF EXISTS {_tableName}";

                using (SqliteCommand command = new SqliteCommand(deleteTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddItem(Product item)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = $"INSERT INTO {_tableName} (Name, Price, Description, Barcode, Quantity) " +
                                    "VALUES (@Name, @Price, @Description, @Barcode, @Quantity)";

                using (SqliteCommand command = new SqliteCommand(insertQuery, connection))
                {
                    if (item is Product product)
                    {
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@Description", product.Description);

                        if (product is BarcodeProduct barcodeProduct)
                        {
                            command.Parameters.AddWithValue("@Barcode", barcodeProduct.Barcode);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Barcode", DBNull.Value);
                        }

                        command.Parameters.AddWithValue("@Quantity", 1);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteItem(Product item)
        {
            string name = item.Name;

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string deleteQuery = $"DELETE FROM {_tableName} WHERE Name = @Name";

                using (SqliteCommand command = new SqliteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddItemWithQuantity(Product item, int quantity)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = $"INSERT INTO {_tableName} (Name, Price, Description, Barcode, Quantity) " +
                                    "VALUES (@Name, @Price, @Description, @Barcode, @Quantity)";

                using (SqliteCommand command = new SqliteCommand(insertQuery, connection))
                {
                    if (item is Product product)
                    {
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@Description", product.Description);

                        if (product is BarcodeProduct barcodeProduct)
                        {
                            command.Parameters.AddWithValue("@Barcode", barcodeProduct.Barcode);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Barcode", DBNull.Value);
                        }

                        command.Parameters.AddWithValue("@Quantity", quantity);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        private Product? ReadFromDataReader(SqliteDataReader reader, string field)
        {
            if (reader.Read())
            {
                Product item = new Product(reader["Name"].ToString(), Convert.ToDecimal(reader["Price"]), reader["Description"].ToString());

                string fieldValue = reader[field].ToString();

                if (!string.IsNullOrEmpty(fieldValue))
                {
                    item = new BarcodeProduct(item, fieldValue);
                }

                return item;
            }

            return null;
        }


        public Product? Get(string field, string value)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = $"SELECT * FROM {_tableName} WHERE {field} = @Value";

                using (SqliteCommand command = new SqliteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Value", value);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        return ReadFromDataReader(reader, field);
                    }
                }
            }
        }

        public Product? Take(string field, string value)
        {
            if (!ProductExists(field, value))
            {
                return default(Product);
            }

            if (!IsQuantitySufficient(field, value, 1))
            {
                return default(Product);
            }

            return UpdateQuantityAndGetProduct(field, value);
        }


        private bool ProductExists(string field, string value)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = $"SELECT * FROM {_tableName} WHERE {field} = @Value";

                using (SqliteCommand selectCommand = new SqliteCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Value", value);

                    using (SqliteDataReader reader = selectCommand.ExecuteReader())
                    {
                        return reader.Read();
                    }
                }
            }
        }

        private bool IsQuantitySufficient(string field, string value, int quantity)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = $"SELECT Quantity FROM {_tableName} WHERE {field} = @Value";

                using (SqliteCommand selectCommand = new SqliteCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Value", value);

                    using (SqliteDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int currentQuantity = Convert.ToInt32(reader["Quantity"]);
                            return currentQuantity >= quantity;
                        }
                    }
                }
            }

            return false;
        }

        private Product? UpdateQuantityAndGetProduct(string field, string value)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string updateQuery = $"UPDATE {_tableName} SET Quantity = Quantity - 1 " +
                                    $"WHERE {field} = @Value";

                using (SqliteCommand updateCommand = new SqliteCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Value", value);

                    updateCommand.ExecuteNonQuery();
                }

                // Вернуть объект Product
                return Get(field, value);
            }
        }
    }
}
