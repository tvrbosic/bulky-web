using Bulky.Models;


namespace Bulky.DataAccess.Repository.Interfaces
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader category);

    }
}
