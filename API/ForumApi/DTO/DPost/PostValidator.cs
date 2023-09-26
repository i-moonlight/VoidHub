using FluentValidation;

namespace ForumApi.DTO.DPost
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(p => p.Content)
            .NotEmpty()
            .WithMessage("Content is required")
            .Length(1, 24000)
            .WithMessage("Content must be between 1 and 24000 characters");   
        }
    }
}