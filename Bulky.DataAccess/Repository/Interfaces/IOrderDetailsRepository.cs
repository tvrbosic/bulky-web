using Bulky.Models;


namespace Bulky.DataAccess.Repository.Interfaces
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
        void Update(OrderDetails category);

    }
}
