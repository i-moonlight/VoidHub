namespace ForumApi.DTO.Auth
{
    public class JwtPair
    {
        public string AccessToken { get;set; } = null!;
        public string RefreshToken { get;set; } = null!;
    }
}