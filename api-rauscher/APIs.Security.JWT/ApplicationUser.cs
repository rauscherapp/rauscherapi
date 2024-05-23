using Microsoft.AspNetCore.Identity;

namespace APIs.Security.JWT;

public class ApplicationUser : IdentityUser
{
    public string? Documento { get; set; }
    public string? NomeCompleto { get; set; }
    public string? Avatar { get; set; }
    public string? Status { get; set; }
    public DateTime? DataNascimento { get; set; }
    public Boolean HasValidStripeSubscription { get; set; }
}