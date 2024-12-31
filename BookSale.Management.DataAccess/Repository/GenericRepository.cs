using BookSale.Management.DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.DataAccess.Repository
{
    public class GenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        //ls.where(x => x.name == "abc").ToList()
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            if (expression is null)
            {
                return await _applicationDbContext.Set<T>().ToListAsync();
            }
            return await _applicationDbContext.Set<T>().Where(expression).ToListAsync();
        }
        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            try {
                return await _applicationDbContext.Set<T>().SingleOrDefaultAsync(expression);
            }
            catch (InvalidOperationException) {
                return null;
            }
        }
        public async Task Create(T entity)
        {
            await _applicationDbContext.Set<T>().AddAsync(entity);
        }
        public void Update(T entity)
        {
            _applicationDbContext.Set<T>().Attach(entity);
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
        }
        public async Task Delete(T entity)
        {
            _applicationDbContext.Set<T>().Attach(entity);
            _applicationDbContext.Entry(entity).State = EntityState.Deleted;
        }
        public async Task Commit()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public IQueryable<T> Table => _applicationDbContext.Set<T>();


    }
}
