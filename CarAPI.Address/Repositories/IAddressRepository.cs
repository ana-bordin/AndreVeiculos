namespace Repositories
{
    public interface IAddressRepository
    {
        //bool InsertAll(List<AddressRepository> adresses);
        //bool Insert(AddressRepository adress);
        List<Models.Address> GetAll();
    }
}
