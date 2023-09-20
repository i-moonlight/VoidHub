using FluentValidation;

namespace ForumApi.DTO.DForum
{
    public class ForumDtoValidator : AbstractValidator<ForumDto>
    {
        public ForumDtoValidator()
        {
            RuleFor(f => f.Title)
                .NotEmpty()
                .WithName("Title is required")
                .Length(3, 255)
                .WithName("Title must be between 3 and 255 characters");
            
            RuleFor(f => f.SectionId)
            .GreaterThanOrEqualTo(1);
        }
    }
}