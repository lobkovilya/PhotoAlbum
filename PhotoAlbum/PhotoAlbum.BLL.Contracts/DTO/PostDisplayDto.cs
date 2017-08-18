using System;

namespace PhotoAlbum.BLL.Contracts.DTO
{
    public class PostDisplayDto
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }
        public int PhotoId { get; set; }
        public int MarksAmount { get; set; }
        //public string PhotoUrl { get; set; }
        public string Login { get; set; }
    }
}