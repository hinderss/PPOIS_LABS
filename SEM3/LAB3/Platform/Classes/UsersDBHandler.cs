using Microsoft.Data.Sqlite;

namespace Platform.Classes
{
    public class UsersDBHandler
    {
        private readonly string _connectionString;

        public UsersDBHandler(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User? GetUserByEmail(string email)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT PasswordHash, Salt FROM Users WHERE Email = @Email";
                    command.Parameters.Add(new SqliteParameter("@Email", email));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string passwordHash = reader.GetString(0);
                            string salt = reader.GetString(1);
                            return new User(email, passwordHash, salt);
                        }
                    }
                }
            }
            return null;
        }

        public List<User> GetSubscribedUsers()
        {
            List<User> subscribedUsers = new List<User>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Email FROM EmailNewsletter WHERE Subscribed = 1";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string email = reader.GetString(0);
                            User user = new User(email, "", "");
                            if (user != null)
                            {
                                subscribedUsers.Add(user);
                            }
                        }
                    }
                }
            }
            return subscribedUsers;
        }

        public void AddSubscribe(string email)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO EmailNewsletter (Email, Subscribed) VALUES (@Email, @Value)";
                    command.Parameters.Add(new SqliteParameter("@Email", email));
                    command.Parameters.Add(new SqliteParameter("@Value", true));

                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddUser(User user)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Users (Email, PasswordHash, Salt) VALUES (@Email, @PasswordHash, @Salt)";
                    command.Parameters.Add(new SqliteParameter("@Email", user.Email));
                    command.Parameters.Add(new SqliteParameter("@PasswordHash", user.PasswordHash));
                    command.Parameters.Add(new SqliteParameter("@Salt", user.Salt));

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool UserExists(string email)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    command.Parameters.Add(new SqliteParameter("@Email", email));

                    int userCount = Convert.ToInt32(command.ExecuteScalar());

                    return userCount > 0;
                }
            }
        }

    }
}

