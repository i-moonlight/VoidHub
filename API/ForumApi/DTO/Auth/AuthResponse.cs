namespace ForumApi.DTO.Auth
{
    public class AuthResponse
    {
        public JwtPair Tokens {get;set;} = null!;
        public AuthUser User {get;set;} = null!;
    }
}