
namespace Shopping.Web.Services
{
    public interface ICatalogService
    {
        [Get("catalog-service/products?pageIndex={pageIndex}&pageSize={pageSize}")]
        Task<GetProductsResponse> GetProducts(int pageIndex, int pageSize);
        [Get("catalog-service/products/category={category}")]
        Task<GetProductsByCategoryResponse> GetProductsByCategory(string category);
        [Get("catalog-service/products/{id}")]
        Task<GetProductByIdResponse> GetProductById(Guid id);
    }
}
