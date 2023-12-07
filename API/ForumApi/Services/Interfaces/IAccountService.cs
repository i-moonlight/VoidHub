using ForumApi.DTO.Auth;
using ForumApi.DTO.DAccount;

namespace ForumApi.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResponse> Get(int id);
        Task<AuthUser> Update(int targetId, int senderId, AccountDto accountDto);
        Task Delete(int id);
    }
}