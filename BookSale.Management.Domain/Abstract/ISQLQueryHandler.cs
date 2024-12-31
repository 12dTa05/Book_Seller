
using Dapper;
using System.Data;

namespace BookSale.Management.Domain.Abstract
{
    public interface ISQLQueryHandler
    {
        Task ExecuteNonReturnAsync(string query, DynamicParameters dynamicParameters, IDbTransaction dbTransaction = null);
        Task<T> ExecuteReturnSingleRowAsync<T>(string query, DynamicParameters dynamicParameters, IDbTransaction dbTransaction = null);
        Task<T?> ExecuteReturnSingleValueScalarAsync<T>(string query, DynamicParameters dynamicParameters, IDbTransaction dbTransaction = null);
        Task<IEnumerable<T>> ExecuteStoreProcedureReturnListAsync<T>(string storeName, DynamicParameters dynamicParameters, IDbTransaction dbTransaction = null);
    }
}