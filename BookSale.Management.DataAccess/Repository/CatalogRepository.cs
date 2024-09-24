using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstract;
using BookSale.Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.DataAccess.Repository
{
    public class CatalogRepository : GenericRepository<Catalogue>, ICatalogRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CatalogRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<Catalogue> Table => base.Table;

        public async Task<IEnumerable<Catalogue>> GetAllCatalogue()
        {
            return await GetAllAsync(x => x.IsActive);
        }
    }
}
