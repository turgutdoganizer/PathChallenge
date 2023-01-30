using Catalog.Dto.Products;
using MediatR;
using PathProjectChallenge.Core.Utilities.Results;

namespace Catalog.Service.Services.Products.Commands
{
    public class ProductUpdateCommand : IRequest<IResult>
    {
        public ProductUpdateDto ProductUpdateDto { get; set; }
    }
}
