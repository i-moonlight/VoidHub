using System.Security.Cryptography;
namespace ForumApi.DTO.Auth
{
    public class Login
    {
        public string LoginName { get;set; } = null!;
        public string Password { get;set; } = null!;
    }
}