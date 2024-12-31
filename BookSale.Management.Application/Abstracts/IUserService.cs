using BookSale.Management.Application.DTOs;
using System.Threading.Tasks;

namespace BookSale.Management.Application.Abstracts
{
    public interface IUserService
    {
        Task<bool> DeleteAsync(string id);
        Task<AccountDTO> GetUserById(string id);
        Task<ResponseDatatable<UserModel>> GetUserByPagination(RequestDatatable requestDatatable);
        Task<ResponseModel> Save(AccountDTO accountDTO);
    }
}