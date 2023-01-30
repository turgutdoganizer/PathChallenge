using Catalog.Service.Services.Products.Commands;
using FluentValidation;

namespace Catalog.Service.Services.Products.Validators
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateCommand>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.ProductUpdateDto.Id).NotEmpty().NotNull();
            RuleFor(x => x.ProductUpdateDto.Name).NotEmpty().NotNull();
            RuleFor(x => x.ProductUpdateDto.Description).NotEmpty().NotNull();

        }
    }
}