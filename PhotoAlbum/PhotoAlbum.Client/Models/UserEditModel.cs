using System.ComponentModel.DataAnnotations;
using StringResources;

namespace PhotoAlbum.Client.Models
{
    public class UserEditModel
    {
        [Required]
        public string Login { get; set; }
        
        [Display(Name = "FirstName", ResourceType = typeof(Resources))]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources))]
        [Required]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Resources))]
        [Required]
        public string Email { get; set; }
    }
}