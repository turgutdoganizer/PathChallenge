using Catalog.Dto.Categories;
using MediatR;
using PathProjectChallenge.Core.Utilities.Results;

namespace Catalog.Service.Services.Categories.Commands
{
    public class CategoryDeleteCommand : IRequest<IResult>
    {
        public CategoryDeleteDto CategoryDeleteDto { get; set; }
    }
}
