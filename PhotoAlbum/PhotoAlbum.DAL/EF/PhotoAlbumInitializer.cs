using System.Data.Entity;
using PhotoAlbum.DAL.Entities;

namespace PhotoAlbum.DAL.EF
{
    public class PhotoAlbumInitializer : DropCreateDatabaseAlways<PhotoAlbumContext>
    {
        protected override void Seed(PhotoAlbumContext context)
        {
            //context.UserProfiles.Add(new UserProfile
            //{
            //    FirstName = "Ivan",
            //    LastName = "Petrov"
            //});
            //context.UserProfiles.Add(new UserProfile
            //{
            //    FirstName = "Andrei",
            //    LastName = "Smirnov"
            //});
            //context.UserProfiles.Add(new UserProfile
            //{
            //    FirstName = "Ivan",
            //    LastName = "Ivanov"
            //});

            base.Seed(context);
        }
    }
}