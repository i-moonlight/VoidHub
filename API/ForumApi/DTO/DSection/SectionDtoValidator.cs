using FluentValidation;

namespace ForumApi.DTO.DSection
{
    public class SectionDtoValidator : AbstractValidator<SectionDto>
    {
        public SectionDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .Length(3, 255)
                .WithMessage("Title must be between 3 and 255 characters");

            RuleFor(s => s.OrderPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Order position must be greater than or equal to 0")
                .LessThan(int.MaxValue);
        }
    }
}