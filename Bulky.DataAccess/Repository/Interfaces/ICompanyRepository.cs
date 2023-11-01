using Bulky.Models;

namespace Bulky.DataAccess.Repository.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company company);
    }
}
