using System.Security.Cryptography;

namespace Platform.Classes
{
    public class Authentication
    {
        private UsersDBHandler _usersDBHandler;

        public Authentication(UsersDBHandler usersDBHandler)
        {
            _usersDBHandler = usersDBHandler;
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }
            return salt;
        }

        private string HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hash);
            }
        }

        public bool Register(string email, string password)
        {
            if (_usersDBHandler.UserExists(email))
            {
                return false;
            }

            byte[] salt = GenerateSalt();

            string passwordHash = HashPassword(password, salt);

            User user = new User(email, passwordHash, Convert.ToBase64String(salt));

            _usersDBHandler.AddUser(user);
            return true;
        }

        public bool Login(string email, string enteredPassword)
        {
            User? user = _usersDBHandler.GetUserByEmail(email);

            if (user != null)
            {
                byte[] saltBytes = Convert.FromBase64String(user.Salt);

                string enteredPasswordHash = HashPassword(enteredPassword, saltBytes);

                if (enteredPasswordHash == user.PasswordHash)
                {
                    return true;
                }
            }

            return false;
        }

    }
}