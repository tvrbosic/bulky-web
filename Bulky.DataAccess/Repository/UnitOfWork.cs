using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.Interfaces;


namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db; 
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
