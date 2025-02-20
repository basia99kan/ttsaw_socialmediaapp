using Microsoft.EntityFrameworkCore;
using ProfileAPI.Entities;

namespace ProfileAPI.Data
{
    public class ProfileDbContext: DbContext
    {
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserProfile> UserProfiles { get; set; }

    
    }
}
