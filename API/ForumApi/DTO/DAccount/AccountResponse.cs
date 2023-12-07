using ForumApi.DTO.DBan;

namespace ForumApi.DTO.DAccount
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int PostsCount { get; set; }
        public int TopicsCount { get; set; }

        public BanDto? Ban { get; set; }
    }
}