using Shopping.Web.Models.Ordering;
using Shopping.Web.Pages;

namespace Shopping.Web.Services
{
    public interface IOrderingService
    {
        [Get("/order-service/orders?pageIndex={pageIndex}&pageSize={pageSize}")]
        Task<GetOrdersResponse> GetOrders(int? pageIndex = 0, int? pageSize = 10);
        [Get("/order-service/orders/customer/{customerId}")]
        Task<GetOrdersByCustomerIdResponse> GetOrdersByCustomerId(Guid customerId);
        [Get("/order-service/orders/{orderName}")]
        Task<GetOrdersByNameResponse> GetOrdersByName(string orderName);
    }
}
