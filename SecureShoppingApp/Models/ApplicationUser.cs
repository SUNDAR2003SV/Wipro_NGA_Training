using Microsoft.AspNetCore.Identity;

namespace SecureShoppingApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string RoleName { get; set; }
    }
}