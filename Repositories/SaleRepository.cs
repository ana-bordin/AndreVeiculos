using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class SaleRepository
    {
        private string _conn { get; set; }
        public SaleRepository()
        {
            _conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }
        public SaleRepository(string connectionString)
        {
            _conn = connectionString;
        }

        public bool InsertAll(List<Sale> sales)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var sale in sales)
                        {
                            var query = "INSERT INTO Sale (LicensePlate, SaleDate, Value, ClientDocument, EmployeeDocument, PaymentId) VALUES (@LicensePlate, @SaleDate, @Value, @ClientDocument, @EmployeeDocument, @PaymentId)";
                            var result = db.Execute(query, new
                            {
                                LicensePlate = sale.Car.LicensePlate,
                                SaleDate = sale.SaleDate,
                                Value = sale.Value,
                                ClientDocument = sale.Client.Document,
                                EmployeeDocument = sale.Employee.Document,
                                PaymentId = sale.Payment.Id
                            }, transaction);

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

        public bool Insert(Sale sale)
        {
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();
                    db.Execute("INSERT INTO Sale (LicensePlate, SaleDate, Value, ClientDocument, EmployeeDocument, PaymentId) VALUES (@LicensePlate, @SaleDate, @Value, @ClientDocument, @EmployeeDocument, @PaymentId)",
                        new
                        {
                            LicensePlate = sale.Car.LicensePlate,
                            SaleDate = sale.SaleDate,
                            Value = sale.Value,
                            ClientDocument = sale.Client.Document,
                            EmployeeDocument = sale.Employee.Document,
                            PaymentId = sale.Payment.Id
                        });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao inserir no banco de dados. Erro:" + e.Message);
                    return false;
                }
            }
        }

        public List<Sale> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT * FROM Sale";
                return db.Query<Sale>(query).ToList();
            }
        }
    }
}
