using Models;

namespace Repositories
{
    public interface IPixTypeRepository
    {
        bool InsertAll(List<PixType> pixTypes);
        bool Insert(PixType pixType);
        List<PixType> GetAll();
    }
}
