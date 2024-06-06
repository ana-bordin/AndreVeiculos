using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CreditCardRepository
    {
        public string _conn { get; set; }
        public CreditCardRepository()
        {
            _conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public bool InsertAll(List<CreditCard> creditCards)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var creditCard in creditCards)
                        {
                            var query = "INSERT INTO CreditCard (CardNumber, CardName, SecurityCode, ExpirationDate) VALUES (@CardNumber, @CardName, @SecurityCode, @ExpirationDate)";
                            var result = db.Execute(query, new { CardNumber = creditCard.CardNumber, CardName = creditCard.CardName, SecurityCode = creditCard.SecurityCode, ExpirationDate = creditCard.ExpirationDate }, transaction);

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

        public bool Insert(CreditCard creditCard)
        {
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();
                    db.Execute("INSERT INTO CreditCard (CardNumber, CardName, SecurityCode, ExpirationDate) VALUES (@CardNumber, @CardName, @SecurityCode, @ExpirationDate)",
                        new { CardNumber = creditCard.CardNumber, CardName = creditCard.CardName, SecurityCode = creditCard.SecurityCode, ExpirationDate = creditCard.ExpirationDate });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao inserir no banco de dados. Erro:" + e.Message);
                    return false;
                }
            }
        }

        public List<CreditCard> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT * FROM CreditCard";
                return db.Query<CreditCard>(query).ToList();
            }
        }
    }
}
