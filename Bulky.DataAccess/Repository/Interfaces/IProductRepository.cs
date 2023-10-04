using Bulky.Models;


namespace Bulky.DataAccess.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);

    }
}
