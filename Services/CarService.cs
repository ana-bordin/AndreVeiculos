using Models;
using Repositories;

namespace Services
{
    public class CarService
    {
        ICarRepository _carRepository;

        public CarService()
        {
            _carRepository = new CarRepository();
        }

        public bool InsertAll(List<Car> cars)
        {
            return _carRepository.InsertAll(cars);
        }

        public bool Insert(Car car)
        {
            return _carRepository.Insert(car);
        }

        public List<Car> GetAll()
        {
            return _carRepository.GetAll();
        }
    }
}
