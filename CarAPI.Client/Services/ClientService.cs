using CarAPI.Client.Utils;
using MongoDB.Driver;

namespace CarAPI.Client.Services
{
    public class ClientService
    {
        private readonly IMongoCollection<Models.Client> _client;
        public ClientService(IProjMongoDBAPIDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _client = database.GetCollection<Models.Client>(settings.AddressCollectionName);
        }
        //public List<AddressService> GetAll() => _address.Find(address => true).ToList();

        //public AddressService Get(string id) => _address.Find<AddressService>(address => address.Id == id).FirstOrDefault();

        public Models.Client Create(Models.Client client)
        {
            _client.InsertOne(client);
            return client;
        }
    }
}
