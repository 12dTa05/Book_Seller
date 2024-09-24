using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewsModel;

namespace BookSale.Management.Application.Abstracts 
{
    public interface IBookService
    {
        Task<ResponseDatatable<BookDTO>> GetBooksByPaginationAsync(RequestDatatable requestDatatable);
        Task<BookViewModel> GetBooksByIdAsync(int id); 
        Task<ResponseModel> SaveAsync(BookViewModel bookViewModel);
        Task<string> GenerateCodeAsync(int number = 10);
        Task<(IEnumerable<BookDTO>, int)> GetBooksForSiteAsync(int genreId, int pageIndex, int pageSize = 10);
        Task<IEnumerable<BookCartDTO>> GetBooksByListCodeAsync(string[] codes);
    }
}