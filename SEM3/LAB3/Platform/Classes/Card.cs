namespace Platform.Classes
{
    public class Card
    {
        private string _name;
        private string _number;
        private ValidUntil _validUntil;
        private string _cvv;

        public string name { set { _name = value; } }
        public string cardnumber { set { _number = value; } }
        public string expirationdate { set { ParseExpirationDate(value); }  }
        public string securitycode { set { _cvv = value; } }

        public bool IsValid(DateTime currentTime)
        {
            return _validUntil.IsValid(currentTime);
        }

        private void ParseExpirationDate(string input)
        {
            string[] parts = input.Split('/');

            if (parts.Length == 2)
            {
                if (int.TryParse(parts[0], out int numerator) && int.TryParse(parts[1], out int denominator))
                {
                    _validUntil = new ValidUntil(numerator, denominator);
                }
                else
                {
                    throw new FormatException("It is not possible to convert substrings to numbers.");
                }
            }
            else
            {
                throw new FormatException("The input string does not contain the '/' character.");
            }
        }

        public override string ToString()
        {
            return $"{_name} - {_number} - {_validUntil} - {_cvv}";
        }
    }
}

