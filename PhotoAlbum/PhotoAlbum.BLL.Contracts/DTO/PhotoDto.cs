namespace PhotoAlbum.BLL.Contracts.DTO
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        //public string Name { get; set; }
        public string Type { get; set; }
    }
}