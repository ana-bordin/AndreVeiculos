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
        private readonly string _conn;

        public PeopleRepository(string connectionString)
        {
            _conn = connectionString;
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
                                TypeStreet = person.Adress.TypeStreet,
                                Street = person.Adress.Street,
                                ZipCode = person.Adress.ZipCode,
                                Neighborhood = person.Adress.Neighborhood,
                                City = person.Adress.City,
                                State = person.Adress.State,
                                Number = person.Adress.Number,
                                Complement = person.Adress.Complement
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

                    var addressQuery = "INSERT INTO Address (TypeStreet, Street, ZipCode, Neighborhood, City, State, Number, Complement) " +
                                       "VALUES (@TypeStreet, @Street, @ZipCode, @Neighborhood, @City, @State, @Number, @Complement);" +
                                       "SELECT CAST(SCOPE_IDENTITY() as int)";
                    var addressId = db.Query<int>(addressQuery, new
                    {
                        TypeStreet = person.Adress.TypeStreet,
                        Street = person.Adress.Street,
                        ZipCode = person.Adress.ZipCode,
                        Neighborhood = person.Adress.Neighborhood,
                        City = person.Adress.City,
                        State = person.Adress.State,
                        Number = person.Adress.Number,
                        Complement = person.Adress.Complement
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
                    person.Adress = address;
                    return person;
                }).ToList();
            }
        }
    }
