using BookSale.Management.Domain.Abstract;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.DataAccess.Dapper
{
    public class SQLQueryHandler : ISQLQueryHandler
    {
        private readonly string _connectionString = string.Empty;

        public SQLQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public async Task ExecuteNonReturnAsync(string query, DynamicParameters dynamicParameters, IDbTransaction dbTransaction = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, param: dynamicParameters, dbTransaction);
            }
        }

        public async Task<T?> ExecuteReturnSingleValueScalarAsync<T>(string query, DynamicParameters dynamicParameters, IDbTransaction dbTransaction = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteScalarAsync<T>(query, param: dynamicParameters, dbTransaction);
            }
        }

        public async Task<T> ExecuteReturnSingleRowAsync<T>(string query, DynamicParameters dynamicParameters, IDbTransaction dbTransaction = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleAsync<T>(query, param: dynamicParameters, dbTransaction);
            }
        }

        public async Task<IEnumerable<T>> ExecuteStoreProcedureReturnListAsync<T>(string query, DynamicParameters dynamicParameters, IDbTransaction dbTransaction = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(query, dynamicParameters, dbTransaction, commandType: CommandType.StoredProcedure);
            }
        }



    }
}
