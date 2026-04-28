using Microsoft.AspNetCore.Identity;

namespace GAMA.CO5.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsApproved { get; set; } = false;
    }
}