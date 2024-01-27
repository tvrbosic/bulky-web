using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.Interfaces;
using Bulky.Models;


namespace Bulky.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader category)
        {
            _db.OrderHeader.Update(category);
        }
    }
}
