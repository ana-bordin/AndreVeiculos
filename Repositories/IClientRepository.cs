using Models;

namespace Repositories
{
    public interface IClientRepository
    {
        bool InsertAll(List<Client> clients);
        bool Insert(Client client);
        List<Client> GetAll();
    }
}
