
namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;

    public record DeleteOrderResult(bool Success);

    public class DeleteCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
