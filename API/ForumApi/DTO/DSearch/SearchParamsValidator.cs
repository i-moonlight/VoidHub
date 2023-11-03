using FluentValidation;

namespace ForumApi.DTO.DSearch
{
    public class SearchParamsValidator : AbstractValidator<SearchParams>
    {
        public SearchParamsValidator() 
        {
            RuleFor(x => x.Sort)
                .Must(x => x == "asc" || x == "desc" || x == "").WithMessage("Sort must be asc, desc or empty");
        }
    }
}