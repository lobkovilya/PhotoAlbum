namespace PhotoAlbum.BLL.Contracts.DTO
{
    public class UserChangePasswordDto
    {
        public string Login { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}