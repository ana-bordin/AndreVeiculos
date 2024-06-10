using Repositories;

namespace CarAPI.Address.Services
{
    public class AddressService
    {
        IAddressRepository _addressRepository;
        
        public AddressService()
        {
            _addressRepository = new AddressRepository();
        }
        public List<Models.Address> GetAll()
        {
            return _addressRepository.GetAll();
        }
    }

}
