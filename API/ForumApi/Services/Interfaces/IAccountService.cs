using ForumApi.DTO.Auth;
using ForumApi.DTO.DAccount;

namespace ForumApi.Services.Interfaces
{
    public interface IAccountService
    {
        Task Delete(int id);
        Task Update(int id, AccountDto accountDto);     
    }
}