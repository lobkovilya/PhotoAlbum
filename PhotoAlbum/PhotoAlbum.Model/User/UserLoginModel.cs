using System.ComponentModel.DataAnnotations;

namespace PhotoAlbum.WebAPI.Models
{
    public class UserLoginModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}