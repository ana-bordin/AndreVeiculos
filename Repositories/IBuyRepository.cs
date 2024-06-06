using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBuyRepository
    {
        bool InsertAll(List<Buy> buys);
        bool Insert(Buy buy);
        List<Buy> GetAll();
    }
}
