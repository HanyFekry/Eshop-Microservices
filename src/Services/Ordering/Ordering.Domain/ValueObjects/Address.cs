

namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string? Email { get; set; }
        public string PostalCode { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        private Address(string firstName, string lastName, string email, string street, string country, string city, string postalCode)
        {
            Street = street; City = city; PostalCode = postalCode; Country = country; FirstName = firstName; LastName = lastName; Email = email;
        }

        public static Address Of(string firstName, string lastName, string email, string street, string country, string city, string postalCode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(street);
            ArgumentException.ThrowIfNullOrWhiteSpace(city);
            return new Address(firstName, lastName, email, street, country, city, postalCode);
        }

    }
}
