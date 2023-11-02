namespace Platform.Classes
{
    public class ValidUntil
    {
        private int _month;
        private int _year;

        public ValidUntil(int month, int year)
        {
            _month = month;
            _year = year;
        }

        public bool IsValid(DateTime dateTime)
        {
            if (dateTime.Year % 100 < _year)
                return true;
            else if (_year == dateTime.Year % 100 && dateTime.Month < _month)
                return true;
            return false;
        }

        public override string ToString()
        {
            return $"{_month}/{_year}";
        }
    }
}
