
namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto OrderDto) : ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool Success);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.OrderDto.Id).NotEmpty().WithMessage("{PropertyName is required.");
            RuleFor(x => x.OrderDto.CustomerId).NotEmpty().WithMessage("{PropertyName is required.");
            RuleFor(x => x.OrderDto.OrderName).NotEmpty().WithMessage("{PropertyName is required.");
            RuleFor(x => x.OrderDto.OrderItems).NotEmpty().WithMessage("{PropertyName is required.");
        }
    }
}
