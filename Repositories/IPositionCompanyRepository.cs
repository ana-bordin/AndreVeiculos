using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPositionCompanyRepository
    {
        bool InsertAll(List<PositionCompany> positionCompanies);
        bool Insert(PositionCompany positionCompany);
        List<PositionCompany> GetAll();
    }
}
