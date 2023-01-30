using Catalog.Dto.Categories;
using MediatR;
using PathProjectChallenge.Core.Utilities.Results;

namespace Catalog.Service.Services.Categories.Commands
{
    public class CategoryUpdateCommand : IRequest<IResult>
    {
        public CategoryUpdateDto CategoryUpdateDto { get; set; }
    }
}
