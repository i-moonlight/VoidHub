using ForumApi.DTO.Auth;

namespace ForumApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Register(Register auth);
        Task<AuthResponse> Login(Login auth);
        Task<AuthResponse> RefreshPair(string refreshToken);
    }
}