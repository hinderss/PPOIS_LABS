using Microsoft.Data.Sqlite;

namespace Minimarket
{
    public class Product
    {
        protected string _name;
        protected decimal _price;
        protected string _description;

        public Product(string name, decimal price, string description)
        {
            _name = name;
            _price = price;
            _description = description;
        }

        public Product()
        {
            _name = "";
            _price = 0;
            _description = "";
        }

        public string Name
        {
            get
            { return _name; }
        }
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public string Description
        {
            get { return _description; }
        }

        public override string ToString()
        {
            return $"{Name} - {Price:C} - {Description}";
        }

        public virtual void Print()
        {
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Цена: {Price:C}");
            Console.WriteLine($"Описание: {Description}");
        }
    }

    public class BarcodeProduct : Product
    {
        private string _barcode;
        public BarcodeProduct(string name, decimal price, string description, string barcode) : base(name, price, description)
        {
            _barcode = barcode;
        }

        public BarcodeProduct(Product baseProduct, string barcode) : base(baseProduct.Name, baseProduct.Price, baseProduct.Description)
        {
            _barcode = barcode;
        }

        public BarcodeProduct() : base() { _barcode = ""; }

        public string Barcode
        {
            get { return _barcode; }
        }

        public override string ToString()
        {
            return $"{Name} - {Barcode} - {Price:C} - {Description}";
        }

        public new void Print()
        {
            base.Print();
            Console.WriteLine($"Код: {Barcode}");
        }
    }
    
    public class MarketFloor
    {
        private MarketFloorDBManager databaseManager;

        public MarketFloor(string connectionString, string tableName)
        {
            databaseManager = new MarketFloorDBManager(connectionString, tableName);
        }

        public Product? GetProductByName(string name)
        {
            return databaseManager.Get("Name", name);
        }

        public Product? TakeProductByName(string name)
        {
            return databaseManager.Take("Name", name);
        }

        public Product? TakeProductByBarcode(string barcode)
        {
            return databaseManager.Take("Barcode", barcode);
        }


        public void AddOneProduct(Product product)
        {
            databaseManager.AddItemWithQuantity(product, 1);
        }

        public void AddProductWithQuantity(Product product, int quantity)
        {
            databaseManager.AddItemWithQuantity(product, quantity);
        }
    }

    public class CustomersCards
    {
        private DiscountCardsManager databaseManager;

        public CustomersCards(string connectionString, string tableName)
        {
            databaseManager = new DiscountCardsManager(connectionString, tableName);
        }

        public void ActivateCard(DiscountCard card)
        {
            card.Activate(DateTime.Now);
            databaseManager.AddItem(card);
        }

        public DiscountCard? GetCardByNumber(string number)
        {
            return databaseManager.Get("Number", number);
        }
    }

    public class Order
    {
        private DiscountCard? _discountCard;
        public List<Product> Products { get; private set; }
        public Order()
        {
            Products = new List<Product>();
            _discountCard = null;
        }

        public DiscountCard CustomerDiscountCard
        {
            set
            {
                _discountCard = value;
            }
        }
        public decimal TotalAmount
        {

            get
            {
                if (_discountCard is not null)
                {
                    return Products.Sum(product => product.Price) * (1 - _discountCard.Discount);
                }
                return Products.Sum(product => product.Price);
            }

        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }

    public class Cashier
    {
        private MarketFloor _assignedWarehouse;
        private CustomersCards _assignedCustomersCards;

        private Order? _currentOrder;
        private PaymentData _payment;

        private decimal _todayDiscount;

        private void VerifyOrder()
        {
            if (_currentOrder == null)
                _currentOrder = new Order();
            return;
        }
        public Cashier(MarketFloor warehouse, PaymentData paymentData, CustomersCards customersCards)
        {
            _assignedWarehouse = warehouse;
            _currentOrder = null;
            _payment = paymentData;
            _todayDiscount = 0;
            _assignedCustomersCards = customersCards;
        }


        public void AddToReceiptByName(string name)
        {
            VerifyOrder();
            var product1 = _assignedWarehouse.TakeProductByName(name);
            if (product1 == null)
                throw new Exception("The product is not found or the quantity is not sufficient");
            else
                _currentOrder.AddProduct(product1);
        }

        public void AddToReceiptByCode(string code)
        {
            VerifyOrder();
            var product1 = _assignedWarehouse.TakeProductByBarcode(code);
            if (product1 == null)
                throw new Exception("The product is not found or the quantity is not sufficient");
            else
                _currentOrder.AddProduct(product1);
        }

        public void ActivateDiscountCard(DiscountCard card)
        {
            _assignedCustomersCards.ActivateCard(card);
        }

        public void ApplyDiscountCard(string number)
        {
            VerifyOrder();
            _currentOrder.CustomerDiscountCard = _assignedCustomersCards.GetCardByNumber(number);
        }
        public decimal TotalOrder
        {
            get
            {
                VerifyOrder();
                return _currentOrder.TotalAmount * (1 + _todayDiscount);
            }
        }

        public void Transaction(string cardNumber, string cvv)
        {
            _payment.ProcessPayment(TotalOrder, cardNumber, cvv);
            CloseOrder();
        }

        private void CloseOrder()
        {
            VerifyOrder();
            _currentOrder = null;
        }
    }

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

    public class PaymentData
    {
        private readonly string _recipient;
        private readonly string _accountNumber;
        private readonly string _bankIdentificationCode;
        private readonly string _taxpayerIdentificationNumber;
        private readonly string _purpose;
        private readonly string _paymentPurposeCode;
        private readonly string _paymentCategoryCode;

        public PaymentData(
            string recipient,
            string accountNumber,
            string bankIdentificationCode,
            string taxpayerIdentificationNumber,
            string purpose,
            string paymentPurposeCode,
            string paymentCategoryCode)
        {
            _recipient = recipient;
            _accountNumber = accountNumber;
            _bankIdentificationCode = bankIdentificationCode;
            _taxpayerIdentificationNumber = taxpayerIdentificationNumber;
            _purpose = purpose;
            _paymentPurposeCode = paymentPurposeCode;
            _paymentCategoryCode = paymentCategoryCode;
        }

        public void ProcessPayment(decimal sum, string cardNumber, string cvv)
        {
            if (!IsValidCardNumber(cardNumber))
            {
                throw new ArgumentException("Card number must be 16 digits.");
            }

            if (!IsValidCvv(cvv))
            {
                throw new ArgumentException("Card CVV must be 3 digits.");
            }

            Console.WriteLine($"Payment processed successfully!\nSum: {sum}\n");

            Console.WriteLine($"Recipient: {_recipient}");
            Console.WriteLine($"Account Number: {_accountNumber}");
            Console.WriteLine($"Bank Identification Code: {_bankIdentificationCode}");
            Console.WriteLine($"Taxpayer Identification Number: {_taxpayerIdentificationNumber}");
            Console.WriteLine($"Purpose: {_purpose}");
            Console.WriteLine($"Payment Purpose Code: {_paymentPurposeCode}");
            Console.WriteLine($"Payment Category Code: {_paymentCategoryCode}");

            Console.WriteLine($"Payer Card Number: {cardNumber}");
            Console.WriteLine($"Payer Card CVV: {cvv}");
        }

        private bool IsValidCardNumber(string cardNumber)
        {
            if (cardNumber.Length != 16 || !cardNumber.All(char.IsDigit))
            {
                return false;
            }

            return true;
        }

        private bool IsValidCvv(string cvv)
        {
            if (cvv.Length != 3 || !cvv.All(char.IsDigit))
            {
                return false;
            }

            return true;
        }

    }
}
