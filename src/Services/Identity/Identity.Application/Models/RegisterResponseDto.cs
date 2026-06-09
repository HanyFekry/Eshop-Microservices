namespace Identity.Application.Models
{
    public record RegisterResponseDto(bool Succeeded, IEnumerable<string>? Errors, string? ConfirmationToken);
}
