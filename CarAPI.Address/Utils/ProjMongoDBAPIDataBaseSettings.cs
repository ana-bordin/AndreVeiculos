namespace CarAPI.Address.Utils
{
    public class ProjMongoDBAPIDataBaseSettings : IProjMongoDBAPIDataBaseSettings
    {
        public string AddressCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
