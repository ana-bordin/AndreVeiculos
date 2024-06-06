using Models;

namespace Repositories
{
    public interface IPositionCompanyRepository
    {
        bool InsertAll(List<PositionCompany> positionCompanies);
        bool Insert(PositionCompany positionCompany);
        List<PositionCompany> GetAll();
    }
}
