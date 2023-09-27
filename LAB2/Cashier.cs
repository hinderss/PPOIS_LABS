using Minimarket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimarket
{
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

    
}
