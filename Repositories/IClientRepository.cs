using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IClientRepository
    {
        bool InsertAll(List<Client> clients);
        bool Insert(Client client);
        List<Client> GetAll();
    }
}
