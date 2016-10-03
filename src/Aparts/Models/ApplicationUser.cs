using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Aparts.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public byte[] UserPic { get; set; }
    }
}
