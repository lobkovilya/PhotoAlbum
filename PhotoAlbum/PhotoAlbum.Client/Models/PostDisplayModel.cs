using System;

namespace PhotoAlbum.Client.Models
{
    public class PostDisplayModel
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }
        public string PhotoUrl { get; set; }
        public int MarksAmount { get; set; }
        public string Login { get; set; }
        public double UserMark { get; set; }
    }
}