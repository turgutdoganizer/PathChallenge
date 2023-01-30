using Catalog.Service.Services.Categories.Commands;
using FluentValidation;

namespace Catalog.Service.Services.Categories.Validators
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateCommand>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.CategoryUpdateDto.Id).NotEmpty().NotNull();
            RuleFor(x => x.CategoryUpdateDto.Name).NotEmpty().NotNull();
            RuleFor(x => x.CategoryUpdateDto.Description).NotEmpty().NotNull();
        }
    }
}
