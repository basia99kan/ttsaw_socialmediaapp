using PostAPI.Entities;
using Microsoft.EntityFrameworkCore;


namespace PostAPI.Data
{
    public class PostDbContext: DbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext> options) : base(options) { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                        .HasOne<Post>()
                        .WithMany(p => p.Comments)
                        .HasForeignKey(c => c.PostId)
                        .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
