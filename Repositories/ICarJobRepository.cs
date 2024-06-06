using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICarJobRepository
    {
        bool InsertAll(List<CarJob> carJobs);
        bool Insert(CarJob carJob);
        List<CarJob> GetAll();
    }
}
