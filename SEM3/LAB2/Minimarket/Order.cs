using System;

namespace Minimarket
{
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
}
