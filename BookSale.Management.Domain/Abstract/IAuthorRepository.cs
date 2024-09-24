using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstract
{
    public interface IAuthorRepository
    {
        IQueryable<Author> Table { get; }

        Task<IEnumerable<Author>> GetAllAuthor();
        Task<Author?> GetById(int id);
    }
}