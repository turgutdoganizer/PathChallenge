using Catalog.Service.Services.Products.Commands;
using FluentValidation;

namespace Catalog.Service.Services.Products.Validators
{
    internal class ProductInsertValidator : AbstractValidator<ProductInsertCommand>
    {
        public ProductInsertValidator()
        {
            RuleFor(x => x.ProductInsertDto.Name).NotEmpty().NotNull();
            RuleFor(x => x.ProductInsertDto.Description).NotEmpty().NotNull();
        }
    }
}
