
namespace Ordering.Application.Exceptions
{
    public class OrderNotFoundException(object id) : NotFoundException("Order", id);
}
