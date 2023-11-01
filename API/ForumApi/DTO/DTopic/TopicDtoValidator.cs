using FluentValidation;

namespace ForumApi.DTO.DTopic
{
    public class TopicDtoValidator : AbstractValidator<TopicDto>
    {
        public TopicDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        }        
    }
}