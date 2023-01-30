using Catalog.Service.Services.Categories.Commands;
using FluentValidation;

namespace Catalog.Service.Services.Categories.Validators
{
    public class CategoryInsertValidator : AbstractValidator<CategoryInsertCommand>
    {
        public CategoryInsertValidator()
        {
            RuleFor(x => x.CategoryInsertDto.Name).NotEmpty().NotNull();
            RuleFor(x => x.CategoryInsertDto.Description).NotEmpty().NotNull();
        }
    }
}
