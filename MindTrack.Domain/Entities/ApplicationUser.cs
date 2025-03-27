using Microsoft.AspNetCore.Identity;


namespace MindTrack.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
