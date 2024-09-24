using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewsModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSale.Management.Application.Abstracts
{
    public interface IGenreService
    {
        Task<GenreViewModel> GetById(int id);
        Task<ResponseDatatable<GenreDTO>> GetGenreByPagination(RequestDatatable requestDatatable);
        Task<IEnumerable<SelectListItem>> GetGenresForDropdownlistAsync();
        IEnumerable<GenreSiteDTO> GetGenresListForSiteAsync();
    }
}