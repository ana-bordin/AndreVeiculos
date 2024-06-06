using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ClientRepository
    {
        private readonly string _conn;

        public ClientRepository(string connectionString)
        {
            _conn = connectionString;
        }

        public bool InsertAll(List<Client> clients)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var client in clients)
                        {
                            var query = "INSERT INTO Client (Document, Name, BirthDate, AddressId, Telephone, Email, PersonIncome, PDF) VALUES (@Document, @Name, @BirthDate, @AddressId, @Telephone, @Email, @PersonIncome, @PDF)";
                            var result = db.Execute(query, new
                            {
                                Document = client.Document,
                                Name = client.Name,
                                BirthDate = client.BirthDate,
                                AddressId = client.Address.Id,
                                Telephone = client.Telephone,
                                Email = client.Email,
                                PersonIncome = client.PersonIncome,
                                PDF = client.PDF
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

        public bool Insert(Client client)
        {
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();
                    db.Execute("INSERT INTO Client (Document, Name, BirthDate, AddressId, Telephone, Email, PersonIncome, PDF) VALUES (@Document, @Name, @BirthDate, @AddressId, @Telephone, @Email, @PersonIncome, @PDF)",
                        new
                        {
                            Document = client.Document,
                            Name = client.Name,
                            BirthDate = client.BirthDate,
                            AddressId = client.Address.Id,
                            Telephone = client.Telephone,
                            Email = client.Email,
                            PersonIncome = client.PersonIncome,
                            PDF = client.PDF
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

        public List<Client> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT * FROM Client";
                return db.Query<Client>(query).ToList();
            }
        }
    }
}
