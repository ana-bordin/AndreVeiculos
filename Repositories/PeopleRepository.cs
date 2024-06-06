using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PeopleRepository
    {
        private string _conn { get; set; }

        public PeopleRepository()
        {
            _conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public bool InsertAll(List<People> people)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var person in people)
                        {
                            var addressQuery = "INSERT INTO Address (TypeStreet, Street, ZipCode, Neighborhood, City, State, Number, Complement) " +
                                               "VALUES (@TypeStreet, @Street, @ZipCode, @Neighborhood, @City, @State, @Number, @Complement);" +
                                               "SELECT CAST(SCOPE_IDENTITY() as int)";
                            var addressId = db.Query<int>(addressQuery, new
                            {
                                TypeStreet = person.Address.TypeStreet,
                                Street = person.Address.Street,
                                ZipCode = person.Address.ZipCode,
                                Neighborhood = person.Address.Neighborhood,
                                City = person.Address.City,
                                State = person.Address.State,
                                Number = person.Address.Number,
                                Complement = person.Address.Complement
                            }, transaction).Single();

                            var peopleQuery = "INSERT INTO People (Document, Name, BirthDate, AddressId, Telephone, Email) VALUES (@Document, @Name, @BirthDate, @AddressId, @Telephone, @Email)";
                            var result = db.Execute(peopleQuery, new
                            {
                                Document = person.Document,
                                Name = person.Name,
                                BirthDate = person.BirthDate,
                                AddressId = addressId,
                                Telephone = person.Telephone,
                                Email = person.Email
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

        public bool Insert(People person)
        {
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();

                    var addressQuery = "INSERT INTO Address (TypeStreet, Street, ZipCode, Neighborhood, City, State, Number, Complement)VALUES (@TypeStreet, @Street, @ZipCode, @Neighborhood, @City, @State, @Number, @Complement); SELECT CAST(SCOPE_IDENTITY() as int)";
                    var addressId = db.Query<int>(addressQuery, new
                    {
                        TypeStreet = person.Address.TypeStreet,
                        Street = person.Address.Street,
                        ZipCode = person.Address.ZipCode,
                        Neighborhood = person.Address.Neighborhood,
                        City = person.Address.City,
                        State = person.Address.State,
                        Number = person.Address.Number,
                        Complement = person.Address.Complement
                    }).Single();

                    var peopleQuery = "INSERT INTO People (Document, Name, BirthDate, AddressId, Telephone, Email) VALUES (@Document, @Name, @BirthDate, @AddressId, @Telephone, @Email)";
                    db.Execute(peopleQuery, new
                    {
                        Document = person.Document,
                        Name = person.Name,
                        BirthDate = person.BirthDate,
                        AddressId = addressId,
                        Telephone = person.Telephone,
                        Email = person.Email
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

        public List<People> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT p.*, a.* FROM People p JOIN Address a ON p.AddressId = a.Id";
                return db.Query<People, Address, People>(query, (person, address) =>
                {
                    person.Address = address;
                    return person;
                }).ToList();
            }
        }
    }
}
