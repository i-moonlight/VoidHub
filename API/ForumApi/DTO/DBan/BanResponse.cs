using ForumApi.Data.Models;

namespace ForumApi.DTO.DBan
{
    public class BanResponse : BanDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public Account Moderator { get; set; } = null!;
        public Account Account { get; set; } = null!;
        public Account UpdatedBy { get; set; } = null!;
    }
}