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
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AuthorRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<Author> Table => base.Table;

        public async Task<IEnumerable<Author>> GetAllAuthor()
        {
            return await GetAllAsync(x => x.IsActive);
        }

        public async Task<Author?> GetById(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }


    }
}
