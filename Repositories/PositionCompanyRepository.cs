﻿using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class PositionCompanyRepository
    {
        public string _conn { get; set; }
        public PositionCompanyRepository()
        {
            _conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public bool InsertAll(List<PositionCompany> positionCompanies)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var positionCompany in positionCompanies)
                        {
                            var query = "INSERT INTO PositionCompany (Description) VALUES (@Description)";
                            var result = db.Execute(query, new { Description = positionCompany.Description }, transaction);

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

        public bool Insert(PositionCompany positionCompany)
        {
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();
                    db.Execute("INSERT INTO PositionCompany (Description) VALUES (@Description)", new { Description = positionCompany.Description });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao inserir no banco de dados. Erro:" + e.Message);
                    return false;
                }
            }
        }

        public List<PositionCompany> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT * FROM PositionCompany";
                return db.Query<PositionCompany>(query).ToList();
            }
        }
    }
}
