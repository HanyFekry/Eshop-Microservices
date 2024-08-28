namespace Shopping.Web.Models.Catalog
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<string> Category { get; set; } = default!;
        public double Price { get; set; }
        public string ImageUrl { get; set; } = default!;

    }

    //public record GetProductsByCategoryRequest(string Category);
    public record GetProductsByCategoryResponse(IEnumerable<ProductModel> ProductDtos);

    public record GetProductByIdResponse(ProductModel Product);

    public record GetProductsRequest(int PageNumber = 1, int PageSize = 10);
    public record GetProductsResponse(IEnumerable<ProductModel> ProductDtos);
}