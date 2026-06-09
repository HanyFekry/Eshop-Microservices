namespace Identity.Application.Models
{
    public record ForgotPasswordResponseDto(bool Succeeded, string? Token);
}
