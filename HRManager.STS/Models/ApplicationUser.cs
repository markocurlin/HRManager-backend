using Microsoft.AspNetCore.Identity;

namespace HRManager.STS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; }
    }
}
