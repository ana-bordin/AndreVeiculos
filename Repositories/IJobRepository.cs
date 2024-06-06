using Models;

namespace Repositories
{
    public interface IJobRepository
    {
        bool InsertAll(List<Job> Services);
        bool Insert(Job services);
        List<Job> GetAll();
    }
}
