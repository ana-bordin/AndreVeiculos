namespace Repositories
{
    public interface IAddress
    {
        bool InsertAll(List<AddressRepository> adresses);
        bool Insert(AddressRepository adress);
        List<AddressRepository> GetAll();
    }
}
