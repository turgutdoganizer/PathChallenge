using MediatR;
using Ordering.Core.Utilities.Results.Abstract;
using Ordering.Dto.Orders;

namespace Ordering.Service.Services.Orders.Commands
{
    public class OrderInsertCommand : IRequest<IResult>
    {
        public OrderInsertDto OrderInsertDto { get; set; }
    }
}
