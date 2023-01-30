using MediatR;
using Ordering.Core.Utilities.Results.Abstract;
using Ordering.Dto.Pagination;

namespace Ordering.Service.Extensions.Pagination
{
    public class PaginationBaseQuery<T> : IRequest<IPaginationResult<T>>
    {
        public PaginationBaseQuery(PaginationDto paginationDto)
        {
            PaginationDto = paginationDto;
        }

        public PaginationDto PaginationDto { get; set; }
    }
}
