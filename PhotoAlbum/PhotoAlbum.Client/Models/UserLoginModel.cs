using System.ComponentModel.DataAnnotations;
using StringResources;

namespace PhotoAlbum.Client.Models
{
    public class UserLoginModel
    {
        [Required]
        [Display(Name = "Login", ResourceType = typeof(Resources))]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}