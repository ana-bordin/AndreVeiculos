using Dapper;
using Microsoft.Data.SqlClient;
using Models;

namespace Repositories
{
    public class BuyRepository
    {
        private readonly string _conn;

        public BuyRepository(string connectionString)
        {
            _conn = connectionString;
        }

        public bool InsertAll(List<Buy> buys)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var buy in buys)
                        {
                            var query = "INSERT INTO Buy (LicensePlate, Value, Date) VALUES (@LicensePlate, @Value, @Date)";
                            var result = db.Execute(query, new { CarId = buy.Car.LicensePlate, Value = buy.Value, Date = buy.Date }, transaction);

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

        public bool Insert(Buy buy)
        {
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();
                    db.Execute("INSERT INTO Buy (LicensePlate, Value, Date) VALUES (@LicensePlate, @Value, @Date)", new { LicensePlate = buy.Car.LicensePlate, Value = buy.Value, Date = buy.Date });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao inserir no banco de dados. Erro:" + e.Message);
                    return false;
                }
            }
        }

        public List<Buy> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT * FROM Buy";
                return db.Query<Buy>(query).ToList();
            }
        }
    }
}
