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
        public DbSet<Occasion> Occasions { get; set; }
        public DbSet<OccasionPhoto> OccasionsPhotos { get; set; }
        public DbSet<OccasionUser> OccasionUsers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<GroupPhoto> GroupPhoto { get; set; }
        public DbSet<Friendship> Friends { get; set; }
        public DbSet<CommentLikes> CommentLikes { get; set; }
        public DbSet<GroupPost> GroupPosts { get; set; }
        public DbSet<OccasionPost> OccasionPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PostLikes>().HasKey(pk => new { pk.ObjectId, pk.LoverId });
            builder.Entity<PhotoLikes>().HasKey(pk => new { pk.ObjectId, pk.LoverId });
            builder.Entity<CommentLikes>().HasKey(pk => new { pk.ObjectId, pk.LoverId });
            builder.Entity<OccasionUser>().HasKey(pk => new {pk.UserId, pk.OccasionId});
            builder.Entity<GroupUser>().HasKey(pk => new {pk.UserId, pk.GroupId});
            builder.Entity<Friendship>().HasKey(pk => new { pk.UserId, pk.FriendId });
            builder.Entity<GroupPost>().HasKey(pk => new { pk.PostId, pk.GroupId });
            builder.Entity<OccasionPost>().HasKey(pk => new { pk.PostId, pk.OccasionId });
        }
    }
}