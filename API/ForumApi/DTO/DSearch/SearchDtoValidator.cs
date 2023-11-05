using FluentValidation;

namespace ForumApi.DTO.DSearch
{
    public class SearchDtoValidator : AbstractValidator<SearchDto>
    {
        public SearchDtoValidator()
        {
            RuleFor(x => x.Query).NotEmpty().WithMessage("Query cannot be empty");
        }
    }
}