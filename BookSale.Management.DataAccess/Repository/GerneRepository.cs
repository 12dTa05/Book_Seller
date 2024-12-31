using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstract;
using BookSale.Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.DataAccess.Repository
{
    public class GerneRepository : GenericRepository<Genre>, IGerneRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GerneRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<Genre> Table => base.Table;
                
        public async Task<IEnumerable<Genre>> GetAllGenre()
        {
            return await GetAllAsync(x => x.IsActive);
        }

        public async Task<Genre?> GetById(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }

        
    }
}
