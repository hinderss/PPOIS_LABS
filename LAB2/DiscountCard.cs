using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimarket
{
    public class DiscountCard
    {
        private string _number;
        private string _cardHolderName;
        private string _cardHolderPhone;
        private DateTime _creationDate;

        public decimal Discount
        {
            get
            {
                return CalculateDiscountPercent() * 0.01M;
            }
        }

        public DiscountCard(string number, string cardHolderName, string cardHolderPhone)
        {
            _number = number;
            _cardHolderName = cardHolderName;
            _cardHolderPhone = cardHolderPhone;
        }

        public DiscountCard(string number, string cardHolderName, string cardHolderPhone, DateTime creationDate)
        {
            _number = number;
            _cardHolderName = cardHolderName;
            _cardHolderPhone = cardHolderPhone;
            _creationDate = creationDate;
        }

        public void Activate(DateTime dateTime)
        {
            _creationDate = dateTime;
        }

        public string CardHolderName
        {
            get { return _cardHolderName; }
        }

        public string CardHolderPhone
        {
            get { return _cardHolderPhone; }
        }

        public string Number
        {
            get { return _number; }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
        }

        public decimal CalculateDiscountPercent()
        {
            TimeSpan cardAge = DateTime.Now - _creationDate;
            int yearsAsCardholder = cardAge.Days / 365;

            if (yearsAsCardholder < 1)
            {
                return 5;
            }
            else if (yearsAsCardholder < 5)
            {
                return 10;
            }
            else
            {
                return 15;
            }
        }
    }
}
