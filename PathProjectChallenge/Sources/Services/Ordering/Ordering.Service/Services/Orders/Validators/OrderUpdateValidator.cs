using FluentValidation;
using Ordering.Service.Services.Orders.Commands;

namespace Ordering.Service.Services.Orders.Validators
{
    public class OrderUpdateValidator : AbstractValidator<OrderUpdateCommand>
    {
        public OrderUpdateValidator()
        {
            RuleFor(x => x.OrderUpdateDto.Id).NotEmpty().NotNull();
            RuleFor(x => x.OrderUpdateDto.CurrencyId).NotEmpty().NotNull();
            RuleFor(x => x.OrderUpdateDto.UnitPrice).NotEmpty().NotNull();
            RuleFor(x => x.OrderUpdateDto.Status).NotEmpty().NotNull();
            RuleFor(x => x.OrderUpdateDto.UserId).NotEmpty().NotNull();
            RuleFor(x => x.OrderUpdateDto.TotalPrice).NotEmpty().NotNull();
            RuleFor(x => x.OrderUpdateDto.TotalAmount).NotEmpty().NotNull();
        }
    }
}
