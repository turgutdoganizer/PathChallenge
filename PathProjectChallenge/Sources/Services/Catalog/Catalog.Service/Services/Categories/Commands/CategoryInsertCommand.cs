using Catalog.Dto.Categories;
using MediatR;
using PathProjectChallenge.Core.Utilities.Results;

namespace Catalog.Service.Services.Categories.Commands
{
    public class CategoryInsertCommand : IRequest<IResult>
    {
        public CategoryInsertDto CategoryInsertDto { get; set; }
    }
}
