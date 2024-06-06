using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPixRepository
    {
        bool InsertAll(List<Pix> pixs);
        bool Insert(Pix pix);
        List<Pix> GetAll();
    }
}
