using Models;
using Services;

namespace Controllers
{
    public class CarController
    {
        private readonly CarService _carService;

        public CarController()
        {
            _carService = new CarService();
        }
        public bool InsertAll(List<Car> cars)
        {
            return _carService.InsertAll(cars);
        }
        public bool Insert(Car car)
        {
            return _carService.Insert(car);
        }
        public List<Car> GetAll()
        {
            return _carService.GetAll();
        }
    }
}
