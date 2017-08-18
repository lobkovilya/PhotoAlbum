using System;

namespace PhotoAlbum.BLL.Contracts.DTO
{
    public class PostCreateDto
    {
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Login { get; set; }

        public byte[] Content { get; set; }
        public string Type { get; set; }
    }
}