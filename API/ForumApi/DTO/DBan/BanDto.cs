namespace ForumApi.DTO.DBan
{
    public class BanDto
    {
        public int AccountId { get; set; }
        public string Reason { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}