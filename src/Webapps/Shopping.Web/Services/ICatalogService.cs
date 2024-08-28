
namespace Shopping.Web.Services
{
    public interface ICatalogService
    {
        [Get("catalog-service/products?pageIndex={pageIndex}&pageSize={pageSize}")]
        Task<GetProductsResponse> GetProductsAsync(int pageIndex, int pageSize);
        [Get("catalog-service/products/category={category}")]
        Task<GetProductsByCategoryResponse> GetProductsByCategoryAsync(string category);
        [Get("catalog-service/products/{id}")]
        Task<GetProductByIdResponse> GetProductByIdAsync(Guid id);
    }
}
