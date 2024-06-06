using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;


namespace Repositories
{
    public class AddressRepository
    {
        private string _conn;
        public AddressRepository()
        {
            _conn = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
        }
        public bool InsertAll(List<Address> addresses)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var address in addresses)
                        {
                            var query = "INSERT INTO Address (TypeStreet, Street, ZipCode, Neighborhood, City, State, Number, Complement) VALUES (@TypeStreet, @Street, @ZipCode, @Neighborhood, @City, @State, @Number, @Complement)";

                            var result = db.Execute(query, new { TypeStreet = address.TypeStreet, Street = address.Street, ZipCode = address.ZipCode, Neighborhood = address.Neighborhood, City = address.City, State = address.State, Number = address.Number, Complement = address.Complement }, transaction);
                            transaction.Commit();
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

        public bool Insert(Address adress)
        {
            throw new NotImplementedException();
        }

        public List<Address> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
