using Models;

namespace Repositories
{
    public interface ICarRepository
    {
        bool InsertAll(List<Car> cars);
        bool Insert(Car car);
        List<Car> GetAll();

    }
}
