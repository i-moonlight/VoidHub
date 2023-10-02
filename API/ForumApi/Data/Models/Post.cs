using System.Text.Json.Serialization;

namespace ForumApi.Data.Models
{
    public class Post
    {
        public int Id {get;set;}
        public int AccountId {get;set;}
        public int TopicId {get;set;}
        public string? Content {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime? DeletedAt {get;set;}

        [JsonIgnore]
        public virtual Topic Topic {get;set;} = null!;
        [JsonIgnore]
        public virtual Account Author {get;set;} = null!;        
    }
}