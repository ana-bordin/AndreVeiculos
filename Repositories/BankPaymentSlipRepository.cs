using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class BankPaymentSlipRepository
    {
        private string _conn { get; set; }
        public BankPaymentSlipRepository()
        {
            _conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public bool InsertAll(List<BankPaymentSlip> bankPaymentSlips)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var bankPaymentSlip in bankPaymentSlips)
                        {
                            var query = "INSERT INTO BankPaymentSlip (Number, ExpirationDate) VALUES (@Number, @ExpirationDate)";
                            var result = db.Execute(query, new { Number = bankPaymentSlip.Number, ExpirationDate = bankPaymentSlip.ExpirationDate }, transaction);

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

        public bool Insert(BankPaymentSlip bankPaymentSlip)
        {        
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();
                    db.Execute("INSERT INTO BankPaymentSlip (Number, ExpirationDate) VALUES (@Number, @ExpirationDate)", new { Number = bankPaymentSlip.Number, ExpirationDate = bankPaymentSlip.ExpirationDate });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao inserir no banco de dados. Erro:" + e.Message);
                    return false;
                }
            }
        }

        public List<BankPaymentSlip> GetAll()
        {       
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT * FROM BankPaymentSlip";
                return db.Query<BankPaymentSlip>(query).AsList();
            }
        }
    }
}
