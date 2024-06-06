using Models;

namespace Utilities
{
    public class CarGenerator
    {
        static Random random = new Random();
        static List<char> chars = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public static string GenerateCarColor()
        {
            List<string> carColors = new List<string> { "Red", "Blue", "Green", "Black", "White", "Silver", "Gray", "Yellow", "Orange", "Brown", "Purple", "Beige", "Cyan", "Magenta" };
            Random random = new Random();
            int randomIndex = random.Next(carColors.Count);
            return carColors[randomIndex];
        }
        public static string GenerateLicensePlate()
        {
            int randomIndex;
            char randomChar;
            string licensePlate = null;
            Random random = new Random();
            for (int i = 0; i < 7; i++)
            {
                if (i < 3 || i == 4)
                {
                    randomIndex = random.Next(chars.Count);
                    randomChar = chars[randomIndex];
                    licensePlate += randomChar;
                }
                else
                {
                    int randomNumber = random.Next(10);
                    licensePlate += randomNumber;
                }
            }
            return licensePlate;
        }
        public static string GenerateCarModel()
        {
            List<string> carModels = new List<string> { "Sedan", "Coupe", "Hatchback", "Convertible", "SUV", "Crossover", "Van", "Truck", "Wagon", "Minivan", "Pickup", "Limousine", "Roadster", "Cabriolet", "MPV", "Compact", "Subcompact", "Mid-size", "Full-size", "Luxury", "Sports", "Muscle", "Pony", "Supermini", "Microcar", "Kei", "City", "Estate", "Station", "Shooting-brake", "Grand", "Executive", "Compact executive", "Compact luxury", "Mid-size luxury", "Full-size luxury", "Grand tourer", "Supercar", "Hypercar", "Electric", "Hybrid", "Plug-in hybrid", "Fuel cell", "Autonomous", "Concept", "Kit car", "Replica", "Restomod", "Tuner", "Hot rod", "Rat rod", "Lowrider", "Donk", "Custom", "Muscle car", "Pony car", "Compact SUV", "Mini SUV", "Mid-size SUV", "Full-size SUV", "Luxury SUV", "Crossover SUV", "Compact crossover", "Mid-size crossover", "Full-size crossover", "Luxury crossover", "Sports SUV", "Supermini SUV", "Micro SUV", "Kei SUV", "City SUV", "Estate SUV", "Station SUV", "Shooting-brake SUV", "Grand SUV", "Executive SUV", "Compact executive SUV", "Compact luxury SUV", "Mid-size luxury SUV", "Full-size luxury SUV", "Grand tourer SUV", "Supercar SUV", "Hypercar SUV", "Electric SUV", "Hybrid SUV", "Plug-in hybrid SUV", "Fuel cell SUV", "Autonomous SUV", "Concept SUV", "Kit car SUV", "Replica SUV", "Restomod SUV", "Tuner SUV", "Hot rod SUV", "Rat rod SUV", "Lowrider SUV", "Donk SUV", "Custom SUV", "Muscle car SUV", "Pony car SUV" };
            Random random = new Random();
            int randomIndex = random.Next(carModels.Count);
            return carModels[randomIndex];
        }
        public static Car GenerateCar()
        {
            Car car = new Car
            {
                LicensePlate = GenerateLicensePlate(),
                Name = GenerateCarModel(),
                ModelYear = random.Next(1900, 2022),
                ManufactureYear = random.Next(1900, 2022),
                Color = GenerateCarColor()
            };
            return car;
        }
    }
}
