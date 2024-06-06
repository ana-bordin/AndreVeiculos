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
    public class EmployeeRepository
    {
        private readonly string _conn;

        public EmployeeRepository(string connectionString)
        {
            _conn = connectionString;
        }

        public bool InsertAll(List<Employee> employees)
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var employee in employees)
                        {
                            var query = "INSERT INTO Employee (Document, Name, BirthDate, AddressId, Telephone, Email, PositionCompanyId, CommissionPercentage, Commission) VALUES (@Document, @Name, @BirthDate, @AddressId, @Telephone, @Email, @PositionCompanyId, @CommissionPercentage, @Commission)";
                            var result = db.Execute(query, new
                            {
                                Document = employee.Document,
                                Name = employee.Name,
                                BirthDate = employee.BirthDate,
                                AddressId = employee.Address.Id,
                                Telephone = employee.Telephone,
                                Email = employee.Email,
                                PositionCompanyId = employee.PositionCompany.Id,
                                CommissionPercentage = employee.CommissionPercentage,
                                Commission = employee.Commission
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

        public bool Insert(Employee employee)
        {
            using (var db = new SqlConnection(_conn))
            {
                try
                {
                    db.Open();
                    db.Execute("INSERT INTO Employee (Document, Name, BirthDate, AddressId, Telephone, Email, PositionCompanyId, CommissionPercentage, Commission) VALUES (@Document, @Name, @BirthDate, @AddressId, @Telephone, @Email, @PositionCompanyId, @CommissionPercentage, @Commission)",
                        new
                        {
                            Document = employee.Document,
                            Name = employee.Name,
                            BirthDate = employee.BirthDate,
                            AddressId = employee.Address.Id,
                            Telephone = employee.Telephone,
                            Email = employee.Email,
                            PositionCompanyId = employee.PositionCompany.Id,
                            CommissionPercentage = employee.CommissionPercentage,
                            Commission = employee.Commission
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

        public List<Employee> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var query = "SELECT * FROM Employee";
                return db.Query<Employee>(query).ToList();
            }
        }
    }
}
