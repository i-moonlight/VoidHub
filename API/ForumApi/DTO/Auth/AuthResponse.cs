namespace ForumApi.DTO.Auth
{
    public class AuthResponse
    {
        public JwtPair Tokens {get;set;} = null!;
        public User User {get;set;} = null!;
    }
}