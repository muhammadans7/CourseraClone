using Microsoft.AspNetCore.Identity;

namespace courseraClone.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } // Add custom properties here
    }
}
