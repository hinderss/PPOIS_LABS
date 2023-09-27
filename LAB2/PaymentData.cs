using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimarket
{
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
