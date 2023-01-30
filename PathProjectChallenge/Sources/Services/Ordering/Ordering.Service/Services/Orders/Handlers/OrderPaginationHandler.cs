using MediatR;
using Ordering.Core.Repositories;
using Ordering.Core.Utilities.Results.Abstract;
using Ordering.Dto.Orders;
using Ordering.Service.Extensions.Pagination;
using Ordering.Service.Extensions.Pagination.Abstract;
using Ordering.Service.Mappers;
using Ordering.Service.Services.Orders.Handlers.Base;

namespace Ordering.Service.Services.Orders.Handlers
{
    public class OrderPaginationHandler : BaseOrderHandler, IRequestHandler<PaginationBaseQuery<OrderDto>, IPaginationResult<OrderDto>>
    {
        private readonly IPaginationUriService _paginationUriService;
        private readonly IPaginationQuery _paginationQuery;
        public OrderPaginationHandler(IOrderRepository orderRepository, IPaginationUriService paginationUriService, IPaginationQuery paginationQuery) : base(orderRepository)
        {
            _paginationUriService = paginationUriService;
            _paginationQuery = paginationQuery;
        }

        public async Task<IPaginationResult<OrderDto>> Handle(PaginationBaseQuery<OrderDto> request, CancellationToken cancellationToken)
        {
            var data = await _orderRepository.GetAllPaginationAsync(request.PaginationDto.PageNumber, request.PaginationDto.PageSize);
            var dataDto = OrderMapper.Mapper.Map<IReadOnlyList<OrderDto>>(data);
            var totalRecords = await _orderRepository.CountAsync();
            _paginationQuery.PageNumber = request.PaginationDto.PageNumber;
            _paginationQuery.PageSize = request.PaginationDto.PageSize;
            return PaginationExtensions.CreatePaginationResult(dataDto, System.Net.HttpStatusCode.OK, _paginationQuery, totalRecords, _paginationUriService);
        }
    }
}
