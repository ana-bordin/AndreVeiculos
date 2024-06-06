using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class CarRepository : ICarRepository
    {
        public string _conn { get; set; }
        public CarRepository()
        {
            _conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public bool InsertAll(List<Car> cars)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var car in cars)
                        {
                            var query = "INSERT INTO Car (LicensePlate, Name, ModelYear, ManufactureYear, Color) VALUES (@LicensePlate, @Name, @ModelYear, @ManufactureYear, @Color)";
                            var result = db.Execute(query, new { LicensePlate = car.LicensePlate, Name = car.Name, ModelYear = car.ModelYear, ManufactureYear = car.ManufactureYear, Color = car.Color }, transaction);

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
        public bool Insert(Car car)
        {
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();
                    db.Execute("INSERT INTO Car (LicensePlate, Name, ModelYear, ManufactureYear, Color) VALUES (@LicensePlate, @Name, @ModelYear, @ManufactureYear, @Color)", new { LicensePlate = car.LicensePlate, Name = car.Name, ModelYear = car.ModelYear, ManufactureYear = car.ManufactureYear, Color = car.Color });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao inserir no banco de dados. Erro:" + e.Message);
                    return false;
                }
            }
        }
        public List<Car> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT * FROM Car";
                return db.Query<Car>(query).ToList();
            }
        }
    }
}
