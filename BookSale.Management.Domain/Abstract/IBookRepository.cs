using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstract
{
    public interface IBookRepository
    {
        Task<(IEnumerable<T>, int)> GetBooksByPagination<T>(int pageIndex, int pageSize, string keyword);
        Task<bool> Save(Book book);
        Task<Book?> GetBooksByIdAsync(int id);
        Task<Book?> GetBooksByCodeAsync(string code);
        Task<(IEnumerable<Book>, int)> GetBooksForSite<T>(int genreId, int pageIndex, int pageSize = 10);
        Task<IEnumerable<Book>> GetBooksByListCodeAsync(string[] codes);
    }
}
