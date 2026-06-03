namespace Identity.Application.Models
{
    public record ResetPasswordResponseDto(bool Succeeded, IEnumerable<string>? Errors);
}
