using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;


namespace Repositories
{
    public class PixRepository
    {
        public string _conn { get; set; }
        public PixRepository()
        {
            _conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public bool InsertAll(List<Pix> pixes)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var pix in pixes)
                        {
                            var query = "INSERT INTO Pix (Key, PixTypeId) VALUES (@Key, @PixTypeId)";
                            var result = db.Execute(query, new { Key = pix.Key, PixTypeId = pix.PixType.Id }, transaction);

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

        public bool Insert(Pix pix)
        {
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();
                    db.Execute("INSERT INTO Pix (Key, PixTypeId) VALUES (@Key, @PixTypeId)", new { Key = pix.Key, PixTypeId = pix.PixType.Id });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao inserir no banco de dados. Erro:" + e.Message);
                    return false;
                }
            }
        }

        public List<Pix> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT * FROM Pix";
                return db.Query<Pix>(query).ToList();
            }
        }
    }
}
