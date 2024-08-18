
namespace Ordering.Domain.Exceptions
{
    public class DomainException(string message) : Exception($"Domain exception: \"{message}\" from Domain layer.")
    {
    }
}
