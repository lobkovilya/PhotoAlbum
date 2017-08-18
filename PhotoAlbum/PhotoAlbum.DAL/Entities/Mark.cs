namespace PhotoAlbum.DAL.Entities
{
    public class Mark
    {
        public int Id { get; set; }

        public int Rate { get; set; }

        public int PostId { get; set; }
        public int? UserProfileId { get; set; }

        public virtual Post Post { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}