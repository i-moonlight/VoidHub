using System.Text.Json.Serialization;

namespace ForumApi.Data.Models
{
    public class Account
    {
        public int Id {get;set;}
        public string Username {get;set;} = null!;
        public string LoginName {get;set;} = null!;
        [JsonIgnore]
        public string Email {get;set;} = null!;
        [JsonIgnore]
        public string PasswordHash {get;set;} = null!;
        public string Role {get;set;} = null!;
        public DateTime LastLoggedAt {get;set;}
        public DateTime? DeletedAt {get;set;}

        [JsonIgnore]
        public List<Token> Tokens {get;set;} = new();
        [JsonIgnore]
        public List<Post>? Posts {get;set;}
        [JsonIgnore]
        public List<Topic>? Topics {get;set;}
    }
}