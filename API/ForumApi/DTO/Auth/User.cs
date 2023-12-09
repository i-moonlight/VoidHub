namespace ForumApi.DTO.Auth
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string AvatarPath { get; set; } = null!;
    }
}