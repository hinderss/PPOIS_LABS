namespace Platform.Classes
{
    public class Payment
    {
        private string _invoice;
        private decimal _revenue;
        private int _sales;

        public decimal Revenue { get { return _revenue; } }
        public decimal Sales { get { return _sales; } }

        public Payment(string invoice)
        {
            _invoice = invoice;
            _revenue = 0;
            _sales = 0;
        }

        public bool PayByCard(Card card, decimal sum)
        {
            Console.WriteLine("This is a payment emulation, in a real project some Payment API should be used...");
            if (card != null && sum > 0)
            {
                Console.WriteLine($"Payment from card - ${sum}");
                Console.WriteLine("Enter 1 to approve the payment, any other char to reject it");
                var status = Console.ReadLine();
                if (status == "1")
                {
                    Console.WriteLine($"${sum} was paid from the Card:\n{card}");
                    _sales++;
                    _revenue += sum;
                    return true;
                }
            }
            Console.WriteLine("Payment reject.");
            return false;
        }
    }
}
