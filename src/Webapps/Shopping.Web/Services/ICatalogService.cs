
namespace Shopping.Web.Services
{
    public interface ICatalogService
    {
        [Get("/catalog-service/products?pageIndex={pageIndex}&pageSize={pageSize}")]
        Task<GetProductsResponse> GetProducts(int? pageIndex = 1, int? pageSize = 10);
        [Get("/catalog-service/products/Category/{category}")]
        Task<GetProductsByCategoryResponse> GetProductsByCategory(string category);
        [Get("/catalog-service/products/{id}")]
        Task<GetProductByIdResponse> GetProductById(Guid id);
    }
}
