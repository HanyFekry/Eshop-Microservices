namespace Shopping.Web.Models.Ordering
{
    public record OrderModel(
        Guid Id,
        Guid CustomerId,
        string OrderName,
        AddressModel ShippingAddress,
        AddressModel BillingAddress,
        PaymentModel Payment,
        IEnumerable<OrderItemModel> OrderItems,
        OrderStatus Status);

    public record AddressModel(
        string FirstName,
        string LastName,
        string Email,
        string Street,
        string Country,
        string City,
        string PostalCode);
    public record PaymentModel(string CardName, string CardNumber, string Expiration, string Cvv, int PaymentMethod);
    public record OrderItemModel(Guid OrderId, Guid ProductId, int Quantity, decimal Price);
    public enum OrderStatus
    {
        Submitted = 1,
        Pending = 2,
        Completed = 3,
        Canceled = 4
    }

    //public record GetOrdersRequest(PaginatedRequest PaginatedRequest);
    public record GetOrdersResponse(PaginatedResult<OrderModel> Orders);

    //public record GetOrdersByNameRequest(string OrderName);
    public record GetOrdersByNameResponse(IEnumerable<OrderModel> Orders);

    public record GetOrdersByCustomerIdResponse(IEnumerable<OrderModel> Orders);

}
