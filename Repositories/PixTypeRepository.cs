using Dapper;
using Microsoft.Data.SqlClient;
using Models;

namespace Repositories
{
    public class PixTypeRepository
    {
        private readonly string _conn;

        public PixTypeRepository(string connectionString)
        {
            _conn = connectionString;
        }

        public bool InsertAll(List<PixType> pixTypes)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var pixType in pixTypes)
                        {
                            var query = "INSERT INTO PixType (Name) VALUES (@Name)";
                            var result = db.Execute(query, new { Name = pixType.Name }, transaction);

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

        public bool Insert(PixType pixType)
        {
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();
                    db.Execute("INSERT INTO PixType (Name) VALUES (@Name)", new { Name = pixType.Name });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao inserir no banco de dados. Erro:" + e.Message);
                    return false;
                }
            }
        }

        public List<PixType> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT * FROM PixType";
                return db.Query<PixType>(query).ToList();
            }
        }
    }
}
