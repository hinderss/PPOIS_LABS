namespace Minimarket
{
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
}
