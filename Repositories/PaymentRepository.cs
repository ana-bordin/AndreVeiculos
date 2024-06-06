using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PaymentRepository
    {
        private readonly string _conn;

        public PaymentRepository(string connectionString)
        {
            _conn = connectionString;
        }

        public bool InsertAll(List<Payment> payments)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var payment in payments)
                        {
                            var query = "INSERT INTO Payment (CreditCardNumber, BankPaymentSlipId, PixId, PaymentDate) VALUES (@CreditCardNumber, @BankPaymentSlipId, @PixId, @PaymentDate)";
                            var result = db.Execute(query, new { CreditCardNumber = payment.CreditCard.CardNumber, BankPaymentSlipId = payment.BankPaymentSlip.Id, PixId = payment.Pix.Id, PaymentDate = payment.PaymentDate }, transaction);

                            if (result == 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Erro ao inserir no banco de dados. Erro:" + e.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool Insert(Payment payment)
        {
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();
                    db.Execute("INSERT INTO Payment (CreditCardNumber, BankPaymentSlipId, PixId, PaymentDate) VALUES (@CreditCardNumber, @BankPaymentSlipId, @PixId, @PaymentDate)",
                        new { CreditCardNumber = payment.CreditCard.CardNumber, BankPaymentSlipId = payment.BankPaymentSlip.Id, PixId = payment.Pix.Id, PaymentDate = payment.PaymentDate });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao inserir no banco de dados. Erro:" + e.Message);
                    return false;
                }
            }
        }

        public List<Payment> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT * FROM Payment";
                return db.Query<Payment>(query).ToList();
            }
        }
    }
}
