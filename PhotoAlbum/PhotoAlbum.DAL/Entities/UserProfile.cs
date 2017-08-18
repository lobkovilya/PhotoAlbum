namespace PhotoAlbum.DAL.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}