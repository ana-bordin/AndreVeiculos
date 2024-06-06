using Models;

namespace Repositories
{
    public interface IPixRepository
    {
        bool InsertAll(List<Pix> pixs);
        bool Insert(Pix pix);
        List<Pix> GetAll();
    }
}
