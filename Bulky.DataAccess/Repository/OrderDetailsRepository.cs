using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.Interfaces;
using Bulky.Models;


namespace Bulky.DataAccess.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private ApplicationDbContext _db;

        public OrderDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetails orderDetails)
        {
            _db.OrderDetails.Update(orderDetails);
        }
    }
}
