
namespace Ordering.Application.Dtos
{
    public record AddressDto(
        string FirstName,
        string LastName,
        string Email,
        string Street,
        string Country,
        string City,
        string PostalCode);
}
