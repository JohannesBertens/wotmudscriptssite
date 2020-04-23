using Microsoft.AspNetCore.Identity;

namespace site.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string PersonalKey { get; set; }
    }
}
