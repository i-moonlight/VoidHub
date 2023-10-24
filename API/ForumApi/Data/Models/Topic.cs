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
        public bool IsPinned {get;set;}
        public bool IsClosed {get;set;}

        [JsonIgnore]
        public virtual Forum Forum {get;set;} = null!;
        [JsonIgnore]
        public virtual List<Post> Posts {get;set;} = new();      
        [JsonIgnore]
        public virtual Account Author {get;set;} = null!;  
    }
}