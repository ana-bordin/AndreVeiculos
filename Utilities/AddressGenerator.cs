using Models;

namespace Utilities
{
    public class AddressGenerator
    {
        static Random random = new Random();
        static List<string> typeStreet = new List<string> { "Street", "Avenue", "Boulevard", "Lane", "Drive" };
        static List<string> street = new List<string> { "Main St", "Second St", "Third St", "Park Ave", "Fifth Ave" };
        static List<string> zipCode = new List<string> { "12345", "23456", "34567", "45678", "56789" };
        static List<string> neighborhood = new List<string> { "Downtown", "Uptown", "Suburb", "Chinatown", "Old Town" };
        static List<string> city = new List<string> { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix" };
        static List<string> state = new List<string> { "NY", "CA", "IL", "TX", "AZ" };
        static List<string> complement = new List<string> { "Apt 1", "Suite 2", "Building 3", "Floor 4", "Unit 5" };

        public static Address GenerateAdress()
        {
            return new Address
            {
                TypeStreet = typeStreet[random.Next(0, typeStreet.Count)],
                Street = street[random.Next(0, street.Count)],
                ZipCode = zipCode[random.Next(0, zipCode.Count)],
                Neighborhood = neighborhood[random.Next(0, neighborhood.Count)],
                City = city[random.Next(0, city.Count)],
                State = state[random.Next(0, state.Count)],
                Number = random.Next(1, 9999),
                Complement = complement[random.Next(0, complement.Count)]
            };
        }
    }
}
