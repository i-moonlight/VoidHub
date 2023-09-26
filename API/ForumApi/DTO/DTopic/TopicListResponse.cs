using ForumApi.DTO.DForum;

namespace ForumApi.DTO.DTopic
{
    public class TopicListResponse
    {
        public ForumDto Forum {get;set;} = null!;

        public List<TopicListElement> Topics {get;set;} = new();   
    }
}