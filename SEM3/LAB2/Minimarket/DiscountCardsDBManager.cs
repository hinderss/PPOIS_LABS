using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimarket
{
    public class DiscountCardsDBManager : IDatabaseManager<DiscountCard>
    {
        private string _connectionString;
        private string _tableName;

        public DiscountCardsDBManager(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
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
}
