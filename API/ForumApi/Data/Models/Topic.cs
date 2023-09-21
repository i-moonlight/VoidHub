using System.Text.Json.Serialization;

namespace ForumApi.Data.Models
{
    public class Topic
    {
        public int Id {get;set;}
        public int ForumId {get;set;}
        public int AccountId {get;set;}
        public string Title {get;set;} = null!;
        public DateTime CreatedAt {get;set;}
        public DateTime? DeletedAt {get;set;}
        public bool IsClosed {get;set;}

        [JsonIgnore]
        public Forum Forum {get;set;} = null!;
        [JsonIgnore]
        public List<Post> Posts {get;set;} = new();      
        [JsonIgnore]
        public Account Author {get;set;} = null!;  
    }
}