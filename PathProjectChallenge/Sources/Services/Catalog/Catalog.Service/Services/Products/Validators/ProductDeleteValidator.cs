using Catalog.Service.Services.Products.Commands;
using FluentValidation;

namespace Catalog.Service.Services.Products.Validators
{
    public class ProductDeleteValidator : AbstractValidator<ProductDeleteCommand>
    {
        public ProductDeleteValidator()
        {
            RuleFor(x => x.ProductDeleteDto.Id).NotEmpty().NotNull();
        }
    }
}
