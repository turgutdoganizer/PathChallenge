using Catalog.Core.EventSourcing.Events.Categories;
using Catalog.Core.EventSourcing.Streams;
using Catalog.Service.Services.Categories.Commands;
using MediatR;
using PathProjectChallenge.Core.Infrastructure.Mapper;
using PathProjectChallenge.Core.Utilities.Results;
using System.Net;

namespace Catalog.Service.Services.Categories.EventHandlers
{
    public class CategoryUpdateHandler : IRequestHandler<CategoryUpdateCommand, IResult>
    {
        private readonly ICategoryStream _orderStream;
        public CategoryUpdateHandler(ICategoryStream orderStream)
        {
            _orderStream = orderStream;
        }

        public async Task<IResult> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            var orderUpdatedEvent = AutoMapperConfiguration.Mapper.Map<CategoryUpdatedEvent>(request.CategoryUpdateDto);
            _orderStream.Updated(orderUpdatedEvent);
            var result = await _orderStream.InsertAsync(Guid.NewGuid());
            return result ? new Result(HttpStatusCode.Created, "Category is updated successfully") : new Result(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
}
