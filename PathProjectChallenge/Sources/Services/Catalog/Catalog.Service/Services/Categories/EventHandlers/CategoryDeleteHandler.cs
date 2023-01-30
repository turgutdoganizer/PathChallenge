using Catalog.Core.EventSourcing.Events.Categories;
using Catalog.Core.EventSourcing.Streams;
using Catalog.Service.Services.Categories.Commands;
using MediatR;
using PathProjectChallenge.Core.Infrastructure.Mapper;
using PathProjectChallenge.Core.Utilities.Results;
using System.Net;

namespace Catalog.Service.Services.Categories.EventHandlers
{
    public class CategoryDeleteHandler : IRequestHandler<CategoryDeleteCommand, IResult>
    {
        private readonly ICategoryStream _categoryStream;
        public CategoryDeleteHandler(ICategoryStream CategoryStream)
        {
            _categoryStream = CategoryStream;
        }

        public async Task<IResult> Handle(CategoryDeleteCommand request, CancellationToken cancellationToken)
        {
            var categoryDeletedEvent = AutoMapperConfiguration.Mapper.Map<CategoryDeletedEvent>(request.CategoryDeleteDto);
            _categoryStream.Deleted(categoryDeletedEvent);
            var result = await _categoryStream.InsertAsync(Guid.NewGuid());
            return result ? new Result(HttpStatusCode.Created, "Category is deleted successfully") : new Result(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
}
