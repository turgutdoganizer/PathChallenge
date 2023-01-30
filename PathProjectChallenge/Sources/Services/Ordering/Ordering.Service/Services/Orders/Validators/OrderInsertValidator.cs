using FluentValidation;
using Ordering.Service.Services.Orders.Commands;

namespace Ordering.Service.Services.Orders.Validators
{
    public class OrderInsertValidator : AbstractValidator<OrderInsertCommand>
    {
        public OrderInsertValidator()
        {
            RuleFor(x => x.OrderInsertDto.CurrencyId).NotEmpty().NotNull();
            RuleFor(x => x.OrderInsertDto.UnitPrice).NotEmpty().NotNull();
            RuleFor(x => x.OrderInsertDto.Status).NotEmpty().NotNull();
            RuleFor(x => x.OrderInsertDto.UserId).NotEmpty().NotNull();
            RuleFor(x => x.OrderInsertDto.TotalPrice).NotEmpty().NotNull();
            RuleFor(x => x.OrderInsertDto.TotalAmount).NotEmpty().NotNull();
        }
    }
}
