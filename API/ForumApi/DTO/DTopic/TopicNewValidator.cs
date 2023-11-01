using FluentValidation;

namespace ForumApi.DTO.DTopic
{
    public class TopicNewValidator : AbstractValidator<TopicNew>
    {
        public TopicNewValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
        }
    }
}