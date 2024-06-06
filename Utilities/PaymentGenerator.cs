﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class PaymentGenerator
    {
        static Random random = new Random();
        public static Payment PaymentGenerate()
        {
            return new Payment()
            {
                PaymentDate = DateTime.Now,
                PaymentValue = 1000,
                PaymentType = "CreditCard",
                CreditCard = CreditCardGenerate()
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
                ExpirationDate = DateOnly.Parse("01-12-2025")
            };
        }

        public static BankPaymentSlip BankPaymentSlipGenerate()
        {
            return new BankPaymentSlip()
            {
                Number = 1234566,
                //"34191.79001 01043.510047 91020.150008 5 12340000010000"
                ExpirationDate = DateOnly.Parse("11-10-2000")
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