using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class CarJobRepository : ICarJobRepository
    {
        public string _conn { get; set; }
        public CarJobRepository()
        {
            _conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public bool InsertAll(List<CarJob> carJobs)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var carJob in carJobs)
                        {
                            var query = "INSERT INTO CarJob (LicensePlate, JobId, Status) VALUES (@LicensePlate, @JobId, @Status)";
                            var result = db.Execute(query, new { LicensePlate = carJob.Car.LicensePlate, ServiceId = carJob.Job.Id, Status = carJob.Status }, transaction);

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
        public bool Insert(CarJob carJob)
        {
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();
                    db.Execute("INSERT INTO CarJob (LicensePlate, JobId, Status) VALUES (@LicensePlate, @JobId, @Status)", new { LicensePlate = carJob.Car.LicensePlate, JobId = carJob.Job.Id, Status = carJob.Status });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao inserir no banco de dados. Erro:" + e.Message);
                    return false;
                }
            }
        }
        public List<CarJob> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT * FROM CarJob";
                return db.Query<CarJob>(query).ToList();
            }
        }
    }
}
