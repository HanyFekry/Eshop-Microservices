

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

        private Address(string street, string city, string postalCode, string country, string firstName,
            string lastName, string email = "")
        {
            Street = street; City = city; PostalCode = postalCode; Country = country; FirstName = firstName; LastName = lastName; Email = email;
        }

        public static Address Of(string street, string city, string postalCode, string country, string firstName,
            string lastName, string email = "")
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(street);
            ArgumentException.ThrowIfNullOrWhiteSpace(city);
            return new Address(street, city, postalCode, country, firstName, lastName, email);
        }

    }
}
