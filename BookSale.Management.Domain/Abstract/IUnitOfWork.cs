using BookSale.Management.Domain.Abstract;

namespace BookSale.Management.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IGerneRepository GerneRepository { get; }

        Task SaveChangesAsync();
        Task Commit();
        
        void Dispose(bool disposing);

    }
}