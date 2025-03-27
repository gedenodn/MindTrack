using Microsoft.AspNetCore.Identity;


namespace MindTrack.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
