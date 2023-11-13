using System.Text.Json.Serialization;
using NpgsqlTypes;

namespace ForumApi.Data.Models
{
    public class Post
    {
        public int Id {get;set;}
        public int AccountId {get;set;}
        public int TopicId {get;set;}
        public int? AncestorId {get;set;}
        public string Content {get;set;} = null!;
        public DateTime CreatedAt {get;set;}
        public DateTime? DeletedAt {get;set;}

        [JsonIgnore]
        public NpgsqlTsVector SearchVector {get;set;} = null!;

        [JsonIgnore]
        public virtual Topic Topic {get;set;} = null!;
        [JsonIgnore]
        public virtual Account Author {get;set;} = null!;

        [JsonIgnore]
        public virtual List<Post> Comments {get;set;} = new();
        [JsonIgnore]
        public virtual Post? Ancestor {get;set;} = null!;
    }
}