using System.Text.Json.Serialization;

namespace ForumApi.Data.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string LoginName { get; set; } = null!;
        [JsonIgnore]
        public string Email { get; set; } = null!;
        [JsonIgnore]
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime LastLoggedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string AvatarPath { get; set; } = null!;

        [JsonIgnore]
        public virtual List<Token> Tokens { get; set; } = new();
        [JsonIgnore]
        public virtual List<Post> Posts { get; set; } = new(); 
        [JsonIgnore]
        public virtual List<Topic> Topics { get; set; } = new();
        [JsonIgnore]
        public virtual List<Ban> RecievedBans { get; set; } = new();
        [JsonIgnore]
        public virtual List<Ban> GivenBans { get; set; } = new();
        [JsonIgnore]
        public virtual List<Ban> UpdatedBans { get; set; } = new();
    }
}