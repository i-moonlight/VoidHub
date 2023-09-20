using System.Security.Claims;
using ForumApi.Data.Models;
using ForumApi.DTO.Auth;

namespace ForumApi.Services.Interfaces
{
    public interface ITokenService
    {
        string Create(List<Claim> claims, DateTime expiresAt, string secret);
        bool Validate(string token, string secret);
        JwtPair CreatePair(Account account);
        Task Revoke(string refreshToken);
    }
}