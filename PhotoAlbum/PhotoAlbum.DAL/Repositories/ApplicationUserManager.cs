using Microsoft.AspNet.Identity;
using PhotoAlbum.DAL.Entities;

namespace PhotoAlbum.DAL.Repositories
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) 
            : base(store)
        {
        }
    }
}