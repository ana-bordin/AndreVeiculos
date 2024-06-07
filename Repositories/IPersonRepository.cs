using Models;

namespace Repositories
{
    public interface IPersonRepository
    {
        bool InsertAll(List<Person> people);
        bool Insert(Person people);
        List<Person> GetAll();
    }
}
