using ForumApi.DTO.Auth;

namespace ForumApi.Services.Interfaces
{
    public interface IAccountService
    {
        Task Delete(int id);        
    }
}