using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        //For direct querying
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostPhoto> PostPhotos { get; set; }
        public DbSet<PostLikes> PostLikes { get; set; }
        public DbSet<PhotoLikes> PhotoLikes { get; set; }
        public DbSet<ShopPhoto> ShopPhotos { get; set; }
        public DbSet<Shop> ShopAdverts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PostLikes>().HasKey(pk => new { pk.ObjectId, pk.LoverId });
            builder.Entity<PhotoLikes>().HasKey(pk => new { pk.ObjectId, pk.LoverId });
        }
    }
}