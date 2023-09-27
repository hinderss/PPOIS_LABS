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
}
