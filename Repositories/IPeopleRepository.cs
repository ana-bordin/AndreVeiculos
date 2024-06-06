using Models;

namespace Repositories
{
    public interface IPeopleRepository
    {
        bool InsertAll(List<People> people);
        bool Insert(People people);
        List<People> GetAll();
    }
}
