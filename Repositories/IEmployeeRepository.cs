using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEmployeeRepository
    {
        bool InsertAll(List<Employee> employees);
        bool Insert(Employee employee);
        List<Employee> GetAll();
    }
}
