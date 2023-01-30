using Catalog.Service.Services.Categories.Commands;
using FluentValidation;

namespace Catalog.Service.Services.Categories.Validators
{
    public class CategoryDeleteValidator : AbstractValidator<CategoryDeleteCommand>
    {
        public CategoryDeleteValidator()
        {
            RuleFor(x => x.CategoryDeleteDto.Id).NotEmpty().NotNull();
        }
    }
}
