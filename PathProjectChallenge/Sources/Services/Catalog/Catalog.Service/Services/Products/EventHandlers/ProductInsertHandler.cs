using Catalog.Core.EventSourcing.Events.Products;
using Catalog.Core.EventSourcing.Streams;
using Catalog.Service.Services.Products.Commands;
using MediatR;
using PathProjectChallenge.Core.Infrastructure.Mapper;
using PathProjectChallenge.Core.Utilities.Results;
using System.Net;

namespace Catalog.Service.Services.Products.EventHandlers
{
    public class ProductInsertHandler : IRequestHandler<ProductInsertCommand, IResult>
    {

        private readonly IProductStream _produtStream;
        public ProductInsertHandler(IProductStream produtStream)
        {
            _produtStream = produtStream;
        }

        public async Task<IResult> Handle(ProductInsertCommand request, CancellationToken cancellationToken)
        {
            var productInsertedEvent = AutoMapperConfiguration.Mapper.Map<ProductInsertedEvent>(request.ProductInsertDto);
            _produtStream.Inserted(productInsertedEvent);
            var result = await _produtStream.InsertAsync(productInsertedEvent.Id);
            return result ? new Result(HttpStatusCode.Created, "Product is created successfully") : new Result(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
}