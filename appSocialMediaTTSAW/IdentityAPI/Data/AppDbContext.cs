using Microsoft.EntityFrameworkCore;
using IdentityAPI.Entities;


namespace IdentityAPI.Data
{
    public class AppDbContext : DbContext
    {
        //Authentication - Role - System Roles -  (UserProfile)
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokensInfos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}

