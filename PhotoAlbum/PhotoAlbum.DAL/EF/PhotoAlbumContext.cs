using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoAlbum.DAL.Entities;

namespace PhotoAlbum.DAL.EF
{
    public class PhotoAlbumContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Mark> Marks { get; set; }

        public PhotoAlbumContext(string connectionString)
            : base(connectionString)
        {
            //Database.SetInitializer(new PhotoAlbumInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Mark>()
            //    .HasRequired(f => f.UserProfile)
            //    .WithRequiredDependent()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Post>().Property(x => x.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<UserProfile>()
                .HasRequired(c => c.ApplicationUser)
                .WithOptional(c => c.UserProfile);

        }
    }
}