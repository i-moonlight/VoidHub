using FluentValidation;

namespace ForumApi.DTO.Page
{
    public class OffsetValidator : AbstractValidator<Offset>
    {
        public OffsetValidator()
        {
            RuleFor(p => p.OffsetNumber)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Offset number must be greater than or equal to 0");

            RuleFor(p => p.Limit)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .WithMessage("Limit must be greater than 0 and less than or equal to 100");
        }
    }
}