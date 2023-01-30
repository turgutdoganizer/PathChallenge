using Catalog.Core.EventSourcing.Events.Products;
using Catalog.Core.EventSourcing.Streams;
using Catalog.Service.Services.Products.Commands;
using MediatR;
using PathProjectChallenge.Core.Infrastructure.Mapper;
using PathProjectChallenge.Core.Utilities.Results;
using System.Net;

namespace Catalog.Service.Services.Products.EventHandlers
{
    public class ProductUpdateHandler : IRequestHandler<ProductUpdateCommand, IResult>
    {

        private readonly IProductStream _produtStream;
        public ProductUpdateHandler(IProductStream produtStream)
        {
            _produtStream = produtStream;
        }

        public async Task<IResult> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var productUpdatedEvent = AutoMapperConfiguration.Mapper.Map<ProductInsertedEvent>(request.ProductUpdateDto);
            _produtStream.Inserted(productUpdatedEvent);
            var result = await _produtStream.InsertAsync(productUpdatedEvent.Id);
            return result ? new Result(HttpStatusCode.Created, "Product is updated successfully") : new Result(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
}
