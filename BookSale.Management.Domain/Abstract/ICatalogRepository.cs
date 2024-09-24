using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstract
{
    public interface ICatalogRepository
    {
        IQueryable<Catalogue> Table { get; }

        Task<IEnumerable<Catalogue>> GetAllCatalogue();
    }
}