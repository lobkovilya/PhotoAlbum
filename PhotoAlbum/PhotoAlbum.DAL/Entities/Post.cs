using System;

namespace PhotoAlbum.DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public int MarksAmount { get; set; }
        public DateTime Date { get; set; }

        public int PhotoId { get; set; }
        public int UserProfileId { get; set; }

        public virtual Photo Photo { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}