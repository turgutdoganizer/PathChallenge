using Catalog.Dto.Products;
using MediatR;
using PathProjectChallenge.Core.Utilities.Results;

namespace Catalog.Service.Services.Products.Commands
{
    public class ProductInsertCommand : IRequest<IResult>
    {
        public ProductInsertDto ProductInsertDto { get; set; }
    }
}
