using ForumApi.DTO.Auth;
using ForumApi.DTO.DAccount;
using SixLabors.ImageSharp;

namespace ForumApi.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResponse> Get(int id);
        Task<AuthUser> Update(int targetId, int senderId, AccountDto accountDto);
        Task<AuthUser> UpdateImg(int accountId, string newPath);
        Task Delete(int id);
    }
}