using Models;

namespace Repositories
{
    public interface ICarJobRepository
    {
        bool InsertAll(List<CarJob> carJobs);
        bool Insert(CarJob carJob);
        List<CarJob> GetAll();
    }
}
