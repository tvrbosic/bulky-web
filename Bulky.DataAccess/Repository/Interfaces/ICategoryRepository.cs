using Bulky.Models;


namespace Bulky.DataAccess.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);

    }
}
