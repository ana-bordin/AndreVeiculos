using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class PersonRepository
    {
        private string _conn { get; set; }

        public PersonRepository()
        {
            _conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }

        public bool InsertAll(List<Person> people)
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

                            var PersonQuery = "INSERT INTO Person (Document, Name, BirthDate, AddressId, Telephone, Email) VALUES (@Document, @Name, @BirthDate, @AddressId, @Telephone, @Email)";
                            var result = db.Execute(PersonQuery, new
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

        public bool Insert(Person person)
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

                    var PersonQuery = "INSERT INTO Person (Document, Name, BirthDate, AddressId, Telephone, Email) VALUES (@Document, @Name, @BirthDate, @AddressId, @Telephone, @Email)";
                    db.Execute(PersonQuery, new
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

        public List<Person> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT p.*, a.* FROM Person p JOIN Address a ON p.AddressId = a.Id";
                return db.Query<Person, Address, Person>(query, (person, address) =>
                {
                    person.Address = address;
                    return person;
                }).ToList();
            }
        }
    }
}
