namespace PhotoAlbum.DAL.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        //public string Name { get; set; }
        public string Type { get; set; }
    }
}