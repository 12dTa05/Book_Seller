using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstract;
using BookSale.Management.Domain.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.DataAccess.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ISQLQueryHandler _sQLQueryHandler;

        public BookRepository(ApplicationDbContext applicationDbContext, ISQLQueryHandler sQLQueryHandler) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _sQLQueryHandler = sQLQueryHandler;
        }

        public async Task<(IEnumerable<T>, int)> GetBooksByPagination<T>(int pageIndex, int pageSize, string keyword)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("keyword", keyword, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            dynamicParameters.Add("pageIndex", pageIndex, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            dynamicParameters.Add("pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            dynamicParameters.Add("totalRecords", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

            var result = await _sQLQueryHandler.ExecuteStoreProcedureReturnListAsync<T>("GetAllBookByPagination", dynamicParameters);

            var totalRecords = dynamicParameters.Get<int>("totalRecords");

            return (result, totalRecords);
        }



        public async Task<Book?> GetBooksByIdAsync(int id)
        {
            try
            {
                return await base.GetSingleAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Book?> GetBooksByCodeAsync(string code)
        {
            return await base.GetSingleAsync(x => x.Code == code);
        }
        public async Task<IEnumerable<Book>> GetBooksByListCodeAsync(string[] codes)
        {
            return await base.GetAllAsync(x => codes.Contains(x.Code));
        }


        public async Task<bool> Save(Book book)
        {
            try
            {
                if (book.Id == 0)
                {
                    await base.Create(book);
                }
                else
                {
                    base.Update(book);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<(IEnumerable<Book>, int)> GetBooksForSite<T>(int genreId, int pageIndex, int pageSize = 10)
        {
            IEnumerable<Book> books;
            
            if (genreId == 0)
            {
                books = await base.GetAllAsync();
            }
            else 
            {
                books = await base.GetAllAsync(x => x.GenreId == genreId);
            }

            var totalRecords = books.Count();

            var result = books
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (result, totalRecords);
        }
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }

}
