using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstract;
using BookSale.Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ISQLQueryHandler _sQLQueryHandler;
        private IGerneRepository? _gerneRepository;
        private IBookRepository? _bookRepository;
        private bool disposedValue;
        public UnitOfWork(ApplicationDbContext applicationDbContext, ISQLQueryHandler sQLQueryHandler)
        {
            _applicationDbContext = applicationDbContext;
            _sQLQueryHandler = sQLQueryHandler;
        }

        public IGerneRepository GerneRepository => _gerneRepository ??= new GerneRepository(_applicationDbContext );
        public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_applicationDbContext, _sQLQueryHandler);
        public async Task Commit()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _applicationDbContext.Dispose();
                }
                disposedValue = true;
            }
        }
        public async Task SaveChangesAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
