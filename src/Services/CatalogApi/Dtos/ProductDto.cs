namespace CatalogApi.Dtos
{
    public record ProductDto(Guid Id, string Name, string Description, List<string> Category, double Price);
}
