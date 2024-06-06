using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAddress
    {
        bool InsertAll(List<AddressRepository> adresses);
        bool Insert(AddressRepository adress);
        List<AddressRepository> GetAll();
    }
}
