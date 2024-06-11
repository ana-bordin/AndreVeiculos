using CarAPI.Address.Utils;
using MongoDB.Driver;

namespace CarAPI.Address.Services
{
    public class AddressService
    {
        private readonly IMongoCollection<Models.Address> _address;
        public AddressService(IProjMongoDBAPIDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _address = database.GetCollection<Models.Address>(settings.AddressCollectionName);
        }
        //public List<AddressService> GetAll() => _address.Find(address => true).ToList();

        //public AddressService Get(string id) => _address.Find<AddressService>(address => address.Id == id).FirstOrDefault();

        public Models.Address Create(Models.Address address)
        {
            _address.InsertOne(address);
            return address;
        }
    }
}
