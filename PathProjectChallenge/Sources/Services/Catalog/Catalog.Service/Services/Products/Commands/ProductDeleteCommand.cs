using Catalog.Dto.Products;
using MediatR;
using PathProjectChallenge.Core.Utilities.Results;

namespace Catalog.Service.Services.Products.Commands
{
    public class ProductDeleteCommand : IRequest<IResult>
    {
        public ProductDeleteDto ProductDeleteDto { get; set; }
    }
}
