using ForumApi.DTO.DForum;

namespace ForumApi.DTO.DSection
{
    public class SectionResponse
    {
        public int Id {get;set;}
        public string Title {get;set;} = null!; 
        public int OrderPosition {get;set;}

        public List<ForumResponse> Forums {get;set;} = new();
    }
}