using Catalog.Core.EventSourcing.Events.Categories;
using Catalog.Core.EventSourcing.Streams;
using Catalog.Service.Services.Categories.Commands;
using MediatR;
using PathProjectChallenge.Core.Infrastructure.Mapper;
using PathProjectChallenge.Core.Utilities.Results;
using System.Net;

namespace Catalog.Service.Services.Categories.EventHandlers
{
    public class CategoryInsertHandler : IRequestHandler<CategoryInsertCommand, IResult>
    {

        private readonly ICategoryStream _categoryStream;
        public CategoryInsertHandler(ICategoryStream categoryStream)
        {
            _categoryStream = categoryStream;
        }

        public async Task<IResult> Handle(CategoryInsertCommand request, CancellationToken cancellationToken)
        {
            var categoryInsertedEvent = AutoMapperConfiguration.Mapper.Map<CategoryInsertedEvent>(request.CategoryInsertDto);
            _categoryStream.Inserted(categoryInsertedEvent);
            var result = await _categoryStream.InsertAsync(categoryInsertedEvent.Id);
            return result ? new Result(HttpStatusCode.Created, "Category is created successfully") : new Result(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
}