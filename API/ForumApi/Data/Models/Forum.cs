using System.Text.Json.Serialization;

namespace ForumApi.Data.Models
{
    public class Forum
    {
        public int Id {get;set;}
        public int SectionId {get;set;}
        public string Title {get;set;} = null!;
        public DateTime? DeletedAt {get;set;}
        public bool IsClosed {get;set;}

        [JsonIgnore]
        public Section Section {get;set;} = null!;        
        [JsonIgnore]
        public List<Topic> Topics {get;set;} = new();
    }
}