using Catalog.Core.EventSourcing.Events.Products;
using Catalog.Core.EventSourcing.Streams;
using Catalog.Service.Services.Products.Commands;
using MediatR;
using PathProjectChallenge.Core.Infrastructure.Mapper;
using PathProjectChallenge.Core.Utilities.Results;
using System.Net;

namespace Catalog.Service.Services.Products.EventHandlers
{
    public class ProductDeleteHandler : IRequestHandler<ProductDeleteCommand, IResult>
    {
        private readonly IProductStream _productStream;
        public ProductDeleteHandler(IProductStream productStream)
        {
            _productStream = productStream;
        }

        public async Task<IResult> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            var productDeletedEvent = AutoMapperConfiguration.Mapper.Map<ProductDeletedEvent>(request.ProductDeleteDto);
            _productStream.Deleted(productDeletedEvent);
            var result = await _productStream.InsertAsync(Guid.NewGuid());
            return result ? new Result(HttpStatusCode.Created, "Product is deleted successfully") : new Result(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
}
