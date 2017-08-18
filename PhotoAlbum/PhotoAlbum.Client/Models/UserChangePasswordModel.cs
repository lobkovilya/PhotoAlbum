using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using StringResources;

namespace PhotoAlbum.Client.Models
{
    public class UserChangePasswordModel
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Login", ResourceType = typeof(Resources))]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources))]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "PasswordError", 
            ErrorMessageResourceType = typeof(Resources), MinimumLength = 6)]
        [Display(Name = "NewPassword", ResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", 
            ErrorMessageResourceName = "ConfirmPasswordError", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources))]
        public string ConfirmNewPassword { get; set; }
    }
}