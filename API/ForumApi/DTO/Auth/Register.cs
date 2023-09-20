namespace ForumApi.DTO.Auth
{
    public class Register
    {
        public string Email { get;set; } = null!;
        public string Username { get;set; } = null!;
        public string LoginName { get;set; } = null!;
        public string Password { get;set; } = null!;

    }
}