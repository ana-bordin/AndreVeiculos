using Models;

namespace Repositories
{
    public interface IEmployeeRepository
    {
        bool InsertAll(List<Employee> employees);
        bool Insert(Employee employee);
        List<Employee> GetAll();
    }
}
