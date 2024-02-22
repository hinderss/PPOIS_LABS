namespace Platform.Classes
{
    public class User
    {
        private string _email;
        private string _passwordHash;
        private string _salt;

        public User(string email, string passwordHash, string salt)
        {
            _email = email;
            _passwordHash = passwordHash;
            _salt = salt;
        }

        public string Email
        {
            get { return _email; }
        }

        public string PasswordHash
        {
            get { return _passwordHash; }
        }

        public string Salt
        {
            get { return _salt; }
        }
    }
}
