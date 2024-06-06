using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class JobRepository : IJobRepository
    {
        private string _conn { get; set; }
        public JobRepository()
        {
            _conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public bool InsertAll(List<Job> jobs)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var job in jobs)
                        {
                            var query = "INSERT INTO Job (Description) VALUES (@Description)";
                            var result = db.Execute(query, new { Description = job.Description }, transaction);

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
        public bool Insert(Job job)
        {
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();
                    db.Execute("INSERT INTO Job (Description) VALUES (@Description)", new { Description = job.Description });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao inserir no banco de dados. Erro:" + e.Message);
                    return false;
                }
            }
        }
        public List<Job> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                return db.Query<Job>("SELECT * FROM Job").ToList();
            }
        }
    }

}
