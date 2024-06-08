using Models;

namespace Utilities
{
    public class PaymentGenerator
    {
        static Random random = new Random();
        public static Payment PaymentGenerate(string type)
        {
            CreditCard creditCard = null;
            Pix pix = null;
            BankPaymentSlip bankPaymentSlip = null;

            if (type == "BankPaymentSlip")
                bankPaymentSlip = BankPaymentSlipGenerate();
            else if (type == "Pix")
                pix = PixGenerate();
            else
                creditCard = CreditCardGenerate();

            return new Payment()
            {
                CreditCard = CreditCardGenerate(),
                BankPaymentSlip = bankPaymentSlip,
                Pix = pix,
                PaymentDate = DateTime.Now
            };
        }
        public static CreditCard CreditCardGenerate()
        {
            string cardNumber = null;
            for (int i = 0; i < 16; i++)
            {
                cardNumber += random.Next(0, 9);
            }
            return new CreditCard()
            {
                CardNumber = cardNumber,
                CardName = "John Doe",
                SecurityCode = "123",
                ExpirationDate = DateTime.Parse("01-12-2025")
            };
        }

        public static BankPaymentSlip BankPaymentSlipGenerate()
        {
            return new BankPaymentSlip()
            {
                Number = 1234566,
                //"34191.79001 01043.510047 91020.150008 5 12340000010000"
                ExpirationDate = DateTime.Parse("11-10-2000")
            };
        }

        public static Pix PixGenerate()
        {
            PixType pixType = new PixType()
            {
                Name = "CPF"
            };
            return new Pix()
            {
                Key = "1234567890123456789012345678901234567890123456789012345678901234",
                PixType = pixType
            };
        }
    }
}
