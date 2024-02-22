using Microsoft.Data.Sqlite;

namespace Platform.Classes
{
    public class CoursesDBHandler
    {
        private string _connectionString;

        public CoursesDBHandler(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Course> GetAll()
        {
            List<Course> courses = new List<Course>();

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Courses";

                using (SqliteCommand command = new SqliteCommand(selectQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Course item = ReadFromDataReaderList(reader);
                            courses.Add(item);
                        }
                    }
                }
            }

            return courses;
        }


        public List<Course> GetByTag(string tag)
        {
            List<Course> courses = new List<Course>();

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Courses WHERE Tag = @Tag";

                using (SqliteCommand command = new SqliteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Tag", tag);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Course item = ReadFromDataReaderList(reader);
                            courses.Add(item);
                        }
                    }
                }
            }

            return courses;
        }

        public List<Course> GetByUser(string email)
        {
            List<Course> courses = new List<Course>();

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Courses.*\r\nFROM Courses\r\nJOIN UserCourses ON Courses.Id = UserCourses.CourseId\r\nWHERE UserCourses.UserEmail = @Email;";

                using (SqliteCommand command = new SqliteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Course item = ReadFromDataReaderList(reader);
                            courses.Add(item);
                        }
                    }
                }
            }

            return courses;
        }

        public void AddUserCourse(int courseId, string userEmail)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var insertCommand = connection.CreateCommand();
                    insertCommand.CommandText = "INSERT INTO UserCourses (UserEmail, CourseId) VALUES (@userEmail, @courseId)";

                    insertCommand.Parameters.AddWithValue("@userEmail", userEmail);
                    insertCommand.Parameters.AddWithValue("@courseId", courseId);

                    try
                    {
                        insertCommand.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public bool DoesUserHaveCourse(string userEmail, int courseId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var selectCommand = connection.CreateCommand();
                selectCommand.CommandText = "SELECT COUNT(*) FROM UserCourses WHERE UserEmail = @userEmail AND CourseId = @courseId";
                selectCommand.Parameters.AddWithValue("@userEmail", userEmail);
                selectCommand.Parameters.AddWithValue("@courseId", courseId);

                int count = Convert.ToInt32(selectCommand.ExecuteScalar());

                return count > 0;
            }
        }

        public Course? GetById(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = $"SELECT * FROM Courses WHERE Id = @Value";

                using (SqliteCommand command = new SqliteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Value", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        return ReadFromDataReader(reader);
                    }
                }
            }
        }

        private Course? ReadFromDataReader(SqliteDataReader reader)
        {
            if (reader.Read())
            {
                Course item = new Course(Convert.ToInt32(reader["Id"]), reader["Name"].ToString(), reader["Description"].ToString(), reader["Skills"].ToString(), reader["Tag"].ToString(), Convert.ToDecimal(reader["Price"]), Convert.ToInt32(reader["Duration"]), reader["Resource"].ToString());

                return item;
            }

            return null;
        }

        private Course ReadFromDataReaderList(SqliteDataReader reader)
        {
            Course item = new Course(
                Convert.ToInt32(reader["Id"]),
                reader["Name"].ToString(),
                reader["Description"].ToString(),
                reader["Skills"].ToString(),
                reader["Tag"].ToString(),
                Convert.ToDecimal(reader["Price"]),
                Convert.ToInt32(reader["Duration"]),
                reader["Resource"].ToString());

            return item;
        }

    }
}
