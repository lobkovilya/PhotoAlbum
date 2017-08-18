using System.ComponentModel.DataAnnotations;
using StringResources;

namespace PhotoAlbum.Client.Models
{
    public class UserCreateModel
    {   
        [Required]   
        [Display(Name = "Login", ResourceType = typeof(Resources))]   
        public string Login { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "PasswordError", 
            ErrorMessageResourceType = typeof(Resources), MinimumLength = 6)]
        [Display(Name = "Password", ResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = "ConfirmPasswordError", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Resources))]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources))]
        [Required]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Email", ResourceType = typeof(Resources))]
        [Required]
        public string Email { get; set; }

    }
}