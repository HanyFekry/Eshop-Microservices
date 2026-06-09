namespace Identity.Domain.Models
{
    public record UserDto(string UserName, string? Email, string[] Roles);
}
