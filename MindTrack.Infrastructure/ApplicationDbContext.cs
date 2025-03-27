using MindTrack.Infrastructure.Identity;
using MindTrack.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MindTrack.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<MoodEntry> MoodEntries { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
    }
}
